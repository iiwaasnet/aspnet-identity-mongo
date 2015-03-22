namespace IntegrationTests
{
    using AspNet.Identity.MongoDB;
    using Microsoft.AspNet.Identity;
    using MongoDB.Driver;
    using NUnit.Framework;
    using System.Threading.Tasks;

    [TestFixture]
    public class EnsureWeCanExtendIdentityRoleTests : UserIntegrationTestsBase
    {
        private RoleManager<IdentityRole> manager;
        private ExtendedIdentityRole role;

        public class ExtendedIdentityRole : IdentityRole
        {
            public string ExtendedField { get; set; }
        }

        [SetUp]
        public void BeforeEachTestAfterBase()
        {
            var context = new RolesContext<IdentityRole>(Roles);
            var roleStore = new RoleStore<IdentityRole>(context);
            manager = new RoleManager<IdentityRole>(roleStore);
            role = new ExtendedIdentityRole
                   {
                       Name = "admin"
                   };
        }

        [Test]
        public async Task Create_ExtendedRoleType_SavesExtraFields()
        {
            role.ExtendedField = "extendedField";

            manager.Create(role);

            var savedRole = (ExtendedIdentityRole) await Roles.Find(_ => true).SingleAsync();
            Expect(savedRole.ExtendedField, Is.EqualTo("extendedField"));
        }

        [Test]
        public void Create_ExtendedRoleType_ReadsExtraFields()
        {
            role.ExtendedField = "extendedField";

            manager.Create(role);

            var savedRole = (ExtendedIdentityRole) manager.FindById(role.Id);
            Expect(savedRole.ExtendedField, Is.EqualTo("extendedField"));
        }
    }
}