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
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using Phytime.Models.Feed;
using Phytime.Models.Options;
using Phytime.Repository;

namespace Phytime.Services
{
    public class EmailService : IHostedService, IDisposable
    {
        private readonly RssSource _rssSource;
        private readonly EmailServiceOptions _options;
        private readonly IFeedRepository _feedRepository;
        private Timer _timer;    

        public EmailService(IOptions<EmailServiceOptions> options, IFeedRepository feedRepository)
        {
            _rssSource = RssSource.getInstance();
            _options = options.Value;
            _feedRepository = feedRepository ?? throw new ArgumentNullException(nameof(feedRepository));
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

        private async void CheckUrls(object state)
        {
            foreach (var source in _rssSource.Sources)
            {
                XmlReader reader = XmlReader.Create(source.Url);
                SyndicationFeed feed = SyndicationFeed.Load(reader);
                reader.Close();
                var rssFeed = await _feedRepository.GetByAsync(source.Url);
                if (rssFeed != null)
                {
                    if (rssFeed.ItemsCount != feed.Items.ToList().Count)
                    {
                        var users = await FindUsersToSend(rssFeed);
                        SendNotifications(users, rssFeed);
                    }
                }
                //needs to be moved to new class
                else
                {
                    await _feedRepository.AddAsync(new Feed { Title = source.Title,
                        Url = source.Url, ItemsCount = feed.Items.ToList().Count });
                }
            }
        }

        private async Task<IEnumerable<User>> FindUsersToSend(Feed feed)
        {
            if(feed == null)
            {
                throw new ArgumentNullException(nameof(feed));
            }
            return (await _feedRepository.GetIncludeAsync(feed)).Users;
        }

        private void SendNotifications(IEnumerable<User> users, Feed feed)
        { 
            foreach (var user in users)
            {
                var message = FormMessage(user.Email, "Records updated", feed.Title);
                SendEmail(message);
            }
        }

        private MimeMessage FormMessage(string email, string subject, string message)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentNullException(nameof(email));
            }
            if (string.IsNullOrEmpty(subject))
            {
                throw new ArgumentNullException(nameof(subject));
            }
            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentNullException(nameof(message));
            }
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("Site administration", _options.Email));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };
            return emailMessage;
        }

        private void SendEmail(MimeMessage emailMessage)
        {
            using (var client = new SmtpClient())
            {
                client.Connect(_options.SMTP, _options.Port, false);
                client.Authenticate(_options.Email, _options.Password);
                client.Send(emailMessage);
                client.Disconnect(true);
            }
        }
    }
}
