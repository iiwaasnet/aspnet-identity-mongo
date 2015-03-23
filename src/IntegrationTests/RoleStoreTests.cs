namespace IntegrationTests
{
    using AspNet.Identity.MongoDB;
    using Microsoft.AspNet.Identity;
    using MongoDB.Bson;
    using MongoDB.Driver;
    using NUnit.Framework;
    using System.Threading.Tasks;

    [TestFixture]
    public class RoleStoreTests : UserIntegrationTestsBase
    {
        [Test]
        public async Task Create_NewRole_Saves()
        {
            var roleName = "admin";
            var role = new IdentityRole(roleName);
            var manager = GetRoleManager();

            manager.Create(role);

            var savedRole = await Roles.Find(_ => true).SingleAsync();
            Expect(savedRole.Name, Is.EqualTo(roleName));
        }

        [Test]
        public void FindByName_SavedRole_ReturnsRole()
        {
            var roleName = "name";
            var role = new IdentityRole {Name = roleName};
            var manager = GetRoleManager();
            manager.Create(role);

            var foundRole = manager.FindByName(roleName);

            Expect(foundRole, Is.Not.Null);
            Expect(foundRole.Name, Is.EqualTo(roleName));
        }

        [Test]
        public void FindById_SavedRole_ReturnsRole()
        {
            var roleId = ObjectId.GenerateNewId().ToString();
            var role = new IdentityRole {Id = roleId, Name = "name"};
            var manager = GetRoleManager();
            manager.Create(role);

            var foundRole = manager.FindById(roleId);

            Expect(foundRole, Is.Not.Null);
            Expect(foundRole.Id, Is.EqualTo(roleId));
        }

        [Test]
        public async Task Delete_ExistingRole_Removes()
        {
            var role = new IdentityRole {Name = "name"};
            var manager = GetRoleManager();
            manager.Create(role);
            Expect(await Roles.Find(_ => true).ToListAsync(), Is.Not.Empty);

            manager.Delete(role);

            Expect(await Roles.Find(_ => true).ToListAsync(), Is.Empty);
        }

        [Test]
        public async Task Update_ExistingRole_Updates()
        {
            var role = new IdentityRole {Name = "name"};
            var manager = GetRoleManager();
            manager.Create(role);
            var savedRole = manager.FindById(role.Id);
            savedRole.Name = "newname";

            manager.Update(savedRole);

            var changedRole = await Roles.Find(_ => true).SingleAsync();
            Expect(changedRole, Is.Not.Null);
            Expect(changedRole.Name, Is.EqualTo("newname"));
        }
    }
}