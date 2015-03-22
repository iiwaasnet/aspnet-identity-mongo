namespace IntegrationTests
{
    using AspNet.Identity.MongoDB;
    using Microsoft.AspNet.Identity;
    using MongoDB.Driver;
    using NUnit.Framework;
    using System.Threading.Tasks;

    [TestFixture]
    public class UserRoleStoreTests : UserIntegrationTestsBase
    {
        [Test]
        public void GetRoles_UserHasNoRoles_ReturnsNoRoles()
        {
            var manager = GetUserManager();
            var user = new IdentityUser {UserName = "bob"};
            manager.Create(user);

            var roles = manager.GetRoles(user.Id);

            Expect(roles, Is.Empty);
        }

        [Test]
        public async Task AddRole_Adds()
        {
            var manager = GetUserManager();
            var user = new IdentityUser {UserName = "bob"};
            manager.Create(user);

            manager.AddToRole(user.Id, "role");

            var savedUser = await Users.Find(_ => true).SingleAsync();
            Expect(savedUser.Roles, Is.EquivalentTo(new[] {"role"}));
            Expect(manager.IsInRole(user.Id, "role"), Is.True);
        }

        [Test]
        public async Task RemoveRole_Removes()
        {
            var manager = GetUserManager();
            var user = new IdentityUser {UserName = "bob"};
            manager.Create(user);
            manager.AddToRole(user.Id, "role");

            manager.RemoveFromRole(user.Id, "role");

            var savedUser = await Users.Find(_ => true).SingleAsync();
            Expect(savedUser.Roles, Is.Empty);
            Expect(manager.IsInRole(user.Id, "role"), Is.False);
        }
    }
}