using Moq;
using Xunit;
using Phytime.Services;
using Phytime.Models;
using MimeKit;
using Microsoft.Extensions.Options;

namespace UnitTestApp.Tests.Services
{
    public class EmailServiceTests
    {
        [Fact]
        public void FormEmailMessage_MessageFieldsEqualsToInputData_True()
        {
            var option = Options.Create(new EmailServiceOptions() { Email = "adminEmail" });
            var repositoryMock = new Mock<IRepository<Feed>>();
            var service = new EmailService(option, repositoryMock.Object);
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
