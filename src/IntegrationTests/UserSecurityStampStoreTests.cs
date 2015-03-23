namespace IntegrationTests
{
	using System.Linq;
	using AspNet.Identity.MongoDB;
	using Microsoft.AspNet.Identity;
	using MongoDB.Driver;
	using NUnit.Framework;
    using System.Threading.Tasks;

	[TestFixture]
	public class UserSecurityStampStoreTests : UserIntegrationTestsBase
	{
		[Test]
		public async Task Create_NewUser_HasSecurityStamp()
		{
			var manager = GetUserManager();
			var user = new IdentityUser {UserName = "bob"};

			manager.Create(user);

			var savedUser = await Users.Find(_ => true).SingleAsync();
			Expect(savedUser.SecurityStamp, Is.Not.Null);
		}

		[Test]
		public void GetSecurityStamp_NewUser_ReturnsStamp()
		{
			var manager = GetUserManager();
			var user = new IdentityUser {UserName = "bob"};
			manager.Create(user);

			var stamp = manager.GetSecurityStamp(user.Id);

			Expect(stamp, Is.Not.Null);
		}
	}
}