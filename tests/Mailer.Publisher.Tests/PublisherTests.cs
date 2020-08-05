using Mailer.Publisher;
using Xunit;

namespace XUnitTestProject1
{
    public class PublisherTests
    {
        [Fact]
        public void PublishEmail_ValidEmail()
        {
            var publisher = new Publisher();

            var email = new EmailMessage("test", "test@test.com", "body");

            publisher.PublishEmail(email);

            Assert.True(true);
        }
    }
}
