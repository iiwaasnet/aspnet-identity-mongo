namespace IntegrationTests
{
    using AspNet.Identity.MongoDB;
    using Microsoft.AspNet.Identity;
    using MongoDB.Driver;
    using NUnit.Framework;
    using System.Threading.Tasks;

    [TestFixture]
    public class EnsureWeCanExtendIdentityUserTests : UserIntegrationTestsBase
    {
        private UserManager<IdentityUser> manager;
        private ExtendedIdentityUser user;

        public class ExtendedIdentityUser : IdentityUser
        {
            public string ExtendedField { get; set; }
        }

        [SetUp]
        public void BeforeEachTestAfterBase()
        {
            var context = new UsersContext<IdentityUser>(Users);
            var userStore = new UserStore<IdentityUser>(context);
            manager = new UserManager<IdentityUser>(userStore);
            user = new ExtendedIdentityUser
                   {
                       UserName = "bob"
                   };
        }

        [Test]
        public async Task Create_ExtendedUserType_SavesExtraFields()
        {
            user.ExtendedField = "extendedField";

            manager.Create(user);

            var savedUser = (ExtendedIdentityUser) await Users.Find(_ => true).SingleAsync();
            Expect(savedUser.ExtendedField, Is.EqualTo("extendedField"));
        }

        [Test]
        public void Create_ExtendedUserType_ReadsExtraFields()
        {
            user.ExtendedField = "extendedField";

            manager.Create(user);

            var savedUser = (ExtendedIdentityUser) manager.FindById(user.Id);
            Expect(savedUser.ExtendedField, Is.EqualTo("extendedField"));
        }
    }
}