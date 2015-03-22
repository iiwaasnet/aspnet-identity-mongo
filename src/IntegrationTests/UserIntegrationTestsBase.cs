namespace IntegrationTests
{
	using AspNet.Identity.MongoDB;
	using Microsoft.AspNet.Identity;
	using MongoDB.Driver;
	using NUnit.Framework;
    using System.Threading.Tasks;

	public class UserIntegrationTestsBase : AssertionHelper
	{
		protected IMongoDatabase Database;
		protected IMongoCollection<IdentityUser> Users;
		protected IMongoCollection<IdentityRole> Roles;
		protected UsersContext<IdentityUser> UsersContext;
		protected RolesContext<IdentityRole> RolesContext;

		[SetUp]
		public void BeforeEachTest()
		{
			var client = new MongoClient("mongodb://localhost:27017");
			Database = client.GetDatabase("identity-testing");
			Users = Database.GetCollection<IdentityUser>("users");
			Roles = Database.GetCollection<IdentityRole>("roles");
            RolesContext = new RolesContext<IdentityRole>(Roles);
            UsersContext = new UsersContext<IdentityUser>(Users);

			 Database.DropCollectionAsync("users").Wait();
			 Database.DropCollectionAsync("roles").Wait();
		}

		protected UserManager<IdentityUser> GetUserManager()
		{
			var store = new UserStore<IdentityUser>(UsersContext);
			return new UserManager<IdentityUser>(store);
		}

		protected RoleManager<IdentityRole> GetRoleManager()
		{
			var store = new RoleStore<IdentityRole>(RolesContext);
			return new RoleManager<IdentityRole>(store);
		}
	}
}