using MimeKit;
using MailKit.Net.Smtp;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Collections.Generic;
using Phytime.Models;
using Microsoft.EntityFrameworkCore;
using System.Xml;
using System.ServiceModel.Syndication;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Phytime.Services
{
    public class EmailService : IHostedService, IDisposable
    {
        private Timer _timer;
        private RssSource _rssSource;
        //private PhytimeContext _context;
        private readonly IServiceScopeFactory _scopeFactory;

        public EmailService(IServiceScopeFactory scopeFactory)
        {
            _rssSource = RssSource.getInstance();
            _scopeFactory = scopeFactory;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(CheckUrls, null, TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(60));

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
            System.Diagnostics.Debug.WriteLine("STARTING!!!");
            using (var scope = _scopeFactory.CreateScope())
            {
                PhytimeContext _context = scope.ServiceProvider.GetRequiredService<PhytimeContext>();
                foreach(var url in _rssSource.Urls)
                {
                    XmlReader reader = XmlReader.Create(url);
                    SyndicationFeed feed = SyndicationFeed.Load(reader);
                    reader.Close();
                    var rssFeed = _context.Feeds.FirstOrDefault(f => f.Url == url);
                    if (rssFeed != null)
                    {
                        if (rssFeed.ItemsCount != feed.Items.ToList().Count)
                        {
                            SendNotifications(url, _rssSource.Titles[_rssSource.Urls.IndexOf(url)]);
                        }
                    }
                    //needs to be moved to new class
                    else
                    {
                        _context.Feeds.Add(new Feed { Url = url, ItemsCount = feed.Items.ToList().Count });
                        _context.SaveChanges();
                    }
                }
            }
            
        }

        public void SendNotifications(string feedUrl, string feedTitle)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                PhytimeContext _context = scope.ServiceProvider.GetRequiredService<PhytimeContext>();
                var rssfeed = _context.Feeds.Include(f => f.Users).FirstOrDefault(f => f.Url == feedUrl);
                foreach (var user in rssfeed.Users)
                {
                    SendEmail(user.Email, "Updating records", feedTitle);
                }
            }
        }

        public void SendEmail(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Site administration", "localhosttest@yandex.by"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.yandex.ru", 25, false);
                client.Authenticate("localhosttest@yandex.by", "748563akim");
                client.Send(emailMessage);

                client.Disconnect(true);
            }
        }

        //**************need to fix*************

        //public async Task SendEmailAsync(string email, string subject, string message)
        //{
        //    var emailMessage = new MimeMessage();

        //    emailMessage.From.Add(new MailboxAddress("Администрация сайта", "localhosttest@yandex.by"));
        //    emailMessage.To.Add(new MailboxAddress("", email));
        //    emailMessage.Subject = subject;
        //    emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
        //    {
        //        Text = message
        //    };

        //    using (var client = new SmtpClient())
        //    {
        //        await client.ConnectAsync("smtp.yandex.ru", 25, false);
        //        await client.AuthenticateAsync("localhosttest@yandex.by", "748563akim");
        //        await client.SendAsync(emailMessage);

        //        await client.DisconnectAsync(true);
        //    }
        //}
    }
}
