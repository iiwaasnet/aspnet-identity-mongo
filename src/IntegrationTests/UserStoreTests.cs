namespace IntegrationTests
{
    using AspNet.Identity.MongoDB;
    using Microsoft.AspNet.Identity;
    using MongoDB.Bson;
    using MongoDB.Driver;
    using NUnit.Framework;
    using System.Threading.Tasks;

    [TestFixture]
    public class UserStoreTests : UserIntegrationTestsBase
    {
        [Test]
        public async Task Create_NewUser_Saves()
        {
            var userName = "name";
            var user = new IdentityUser {UserName = userName};
            var manager = GetUserManager();

            manager.Create(user);

            var savedUser = await Users.Find(_ => true).SingleAsync();
            Expect(savedUser.UserName, Is.EqualTo(user.UserName));
        }

        [Test]
        public void FindByName_SavedUser_ReturnsUser()
        {
            var userName = "name";
            var user = new IdentityUser {UserName = userName};
            var manager = GetUserManager();
            manager.Create(user);

            var foundUser = manager.FindByName(userName);

            Expect(foundUser, Is.Not.Null);
            Expect(foundUser.UserName, Is.EqualTo(userName));
        }

        [Test]
        public void FindByName_NoUser_ReturnsNull()
        {
            var manager = GetUserManager();

            var foundUser = manager.FindByName("nouserbyname");

            Expect(foundUser, Is.Null);
        }

        [Test]
        public void FindById_SavedUser_ReturnsUser()
        {
            var userId = ObjectId.GenerateNewId().ToString();
            var user = new IdentityUser {UserName = "name", Id = userId};
            //user.SetId(userId);
            var manager = GetUserManager();
            manager.Create(user);

            var foundUser = manager.FindById(userId);

            Expect(foundUser, Is.Not.Null);
            Expect(foundUser.Id, Is.EqualTo(userId));
        }

        [Test]
        public void FindById_NoUser_ReturnsNull()
        {
            var manager = GetUserManager();

            var foundUser = manager.FindById(ObjectId.GenerateNewId().ToString());

            Expect(foundUser, Is.Null);
        }

        [Test]
        public async Task Delete_ExistingUser_Removes()
        {
            var user = new IdentityUser {UserName = "name"};
            var manager = GetUserManager();
            manager.Create(user);
            Expect(await Users.Find(_ => true).ToListAsync(), Is.Not.Empty);

            manager.Delete(user);

            Expect(await Users.Find(_ => true).ToListAsync(), Is.Empty);
        }

        [Test]
        public async Task Update_ExistingUser_Updates()
        {
            var user = new IdentityUser {UserName = "name"};
            var manager = GetUserManager();
            manager.Create(user);
            var savedUser = manager.FindById(user.Id);
            savedUser.UserName = "newname";

            manager.Update(savedUser);

            var changedUser = await Users.Find(_ => true).SingleAsync();
            Expect(changedUser, Is.Not.Null);
            Expect(changedUser.UserName, Is.EqualTo("newname"));
        }
    }
}