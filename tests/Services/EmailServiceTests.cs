using Moq;
using Xunit;
using Phytime.Services;
using Phytime.Models;
using Microsoft.Extensions.Configuration;
using System.Linq;
using MimeKit;

namespace UnitTestApp.Tests.Services
{
    public class EmailServiceTests
    {
        [Fact]
        public void FormEmailMessageTest()
        {
            var mockConf = new Mock<IConfiguration>();
            var mockRep = new Mock<IRepository>();
            mockConf.Setup(conf => conf.GetSection("AdminEmailParametrs:email").Value).Returns("adminEmail");
            var service = new EmailService(mockConf.Object, mockRep.Object);
            string email = "testMail";
            string sub = "testSubject";
            string mess = "testMessage";

            var message = service.FormMessage(email, sub, mess);

            Assert.Contains(new MailboxAddress("", email), message.To);
            Assert.Equal(sub, message.Subject.ToString());
            Assert.Equal(mess, ((TextPart) message.Body).Text);
        }
    }
}
