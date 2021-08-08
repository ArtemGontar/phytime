using MimeKit;
using MailKit.Net.Smtp;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using Phytime.Models;
using System.Xml;
using System.ServiceModel.Syndication;
using System.Linq;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Phytime.Services
{
    public class EmailService : IHostedService, IDisposable
    {
        private readonly RssSource _rssSource;
        private readonly IConfiguration _config;
        private readonly IRepository _repository;
        private Timer _timer;    

        public EmailService(IConfiguration config, IRepository repository = null)
        {
            _rssSource = RssSource.getInstance();
            _config = config;
            _repository = repository ?? new PhytimeRepository(config);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(CheckUrls, null, TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(60));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

        public void CheckUrls(object state)
        {
            foreach (var url in _rssSource.Urls)
            {
                XmlReader reader = XmlReader.Create(url);
                SyndicationFeed feed = SyndicationFeed.Load(reader);
                reader.Close();
                var rssFeed = _repository.GetFeedByUrl(url);
                if (rssFeed != null)
                {
                    if (rssFeed.ItemsCount != feed.Items.ToList().Count)
                    {
                        var users = FindUsersToSend(rssFeed);
                        SendNotifications(users, rssFeed);
                    }
                }
                //needs to be moved to new class
                else
                {
                    _repository.Add(new Feed { Title = _rssSource.Titles[_rssSource.Urls.IndexOf(url)],
                        Url = url, ItemsCount = feed.Items.ToList().Count });
                }
            }
        }

        public List<User> FindUsersToSend(Feed feed)
        {
            return _repository.GetFeedIncudeUsers(feed).Users;
        }

        public void SendNotifications(List<User> users, Feed feed)
        { 
            foreach (var user in users)
            {
                var message = FormMessage(user.Email, "Records updated", feed.Title);
                SendEmail(message);
            }
        }

        public MimeMessage FormMessage(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("Site administration", 
                _config.GetSection("AdminEmailParametrs:email").Value));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };
            return emailMessage;
        }

        public void SendEmail(MimeMessage emailMessage)
        {
            using (var client = new SmtpClient())
            {
                client.Connect(_config["AdminEmailParametrs:smtp"], 
                    int.Parse(_config["AdminEmailParametrs:port"]), false);
                client.Authenticate(_config["AdminEmailParametrs:email"], 
                    _config["AdminEmailParametrs:password"]);
                client.Send(emailMessage);

                client.Disconnect(true);
            }
        }
    }
}
