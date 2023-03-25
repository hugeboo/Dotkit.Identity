using Dotkit.Identity.Persistence.Services;

namespace Dotkit.Identity.Tests
{
    public class PersistenceTests
    {
        [Fact]
        public void GenerateAndVerifyHash()
        {
            IPasswordHasher ph = new PasswordHasher();
            var hash = ph.HashPassword("!!!QQQ222");
            //AQAAAAEAACcQAAAAECOLAt0sJPcrCWeLX5uE8J1yYFVd+XP09qlDhMr3kNJNgI/azs3Z+7WvmlJuZF15pg==
            var ok = ph.VerifyHashedPassword(hash, "!!!QQQ222");
            Assert.True(ok);
        }
    }
}