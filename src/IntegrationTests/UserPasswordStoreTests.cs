﻿namespace IntegrationTests
{
    using AspNet.Identity.MongoDB;
    using Microsoft.AspNet.Identity;
    using MongoDB.Driver;
    using NUnit.Framework;
    using System.Threading.Tasks;

    [TestFixture]
    public class UserPasswordStoreTests : UserIntegrationTestsBase
    {
        [Test]
        public void HasPassword_NoPassword_ReturnsFalse()
        {
            var user = new IdentityUser {UserName = "bob"};
            var manager = GetUserManager();
            manager.Create(user);

            var hasPassword = manager.HasPassword(user.Id);

            Expect(hasPassword, Is.False);
        }

        [Test]
        public void AddPassword_NewPassword_CanFindUserByPassword()
        {
            var user = new IdentityUser {UserName = "bob"};
            var manager = GetUserManager();
            manager.Create(user);

            manager.AddPassword(user.Id, "testtest");

            var findUserByPassword = manager.Find("bob", "testtest");
            Expect(findUserByPassword, Is.Not.Null);
        }

        [Test]
        public async Task RemovePassword_UserWithPassword_SetsPasswordNull()
        {
            var user = new IdentityUser {UserName = "bob"};
            var manager = GetUserManager();
            manager.Create(user);
            manager.AddPassword(user.Id, "testtest");

            manager.RemovePassword(user.Id);

            var savedUser = await Users.Find(_ => true).SingleAsync();
            Expect(savedUser.PasswordHash, Is.Null);
        }
    }
}