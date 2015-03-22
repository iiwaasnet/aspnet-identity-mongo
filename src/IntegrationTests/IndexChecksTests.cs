namespace IntegrationTests
{
    using System.Linq;
    using AspNet.Identity.MongoDB;
    using MongoDB.Bson.Serialization;
    using MongoDB.Driver;
    using NUnit.Framework;
    using System.Threading.Tasks;

    [TestFixture]
    public class IndexChecksTests : UserIntegrationTestsBase
    {
        [Test]
        public async Task EnsureUniqueIndexOnUserName_NoIndexOnUserName_AddsUniqueIndexOnUserName()
        {
            var userCollectionName = "userindextest";
            await Database.DropCollectionAsync(userCollectionName);
            var users = Database.GetCollection<IdentityUser>(userCollectionName);

            await users.EnsureUniqueIndexOnUserName();

            var indexes = await (await users.Indexes.ListAsync()).ToListAsync();
            var index = indexes.Select(d => BsonSerializer.Deserialize<IndexDescription>(d))
                               .Where(i => i.Unique)
                               .Where(i => i.Key.Count() == 1)
                               .First(i => i.Key.Contains("UserName"));
            Expect(index.Key.Count(), Is.EqualTo(1));
        }

        [Test]
        public async Task EnsureEmailUniqueIndex_NoIndexOnEmail_AddsUniqueIndexOnEmail()
        {
            var userCollectionName = "userindextest";
            await Database.DropCollectionAsync(userCollectionName);
            var users = Database.GetCollection<IdentityUser>(userCollectionName);

            await users.EnsureUniqueIndexOnEmail();

            var indexes = await (await users.Indexes.ListAsync()).ToListAsync();
            var index = indexes.Select(d => BsonSerializer.Deserialize<IndexDescription>(d))
                               .Where(i => i.Unique)
                               .Where(i => i.Key.Count() == 1)
                               .First(i => i.Key.Contains("Email"));
            Expect(index.Key.Count(), Is.EqualTo(1));
        }

        [Test]
        public async Task EnsureUniqueIndexOnRoleName_NoIndexOnRoleName_AddsUniqueIndexOnRoleName()
        {
            var roleCollectionName = "roleindextest";
            await Database.DropCollectionAsync(roleCollectionName);
            var roles = Database.GetCollection<IdentityRole>(roleCollectionName);

            await roles.EnsureUniqueIndexOnRoleName();

            var indexes = await (await roles.Indexes.ListAsync()).ToListAsync();
            var index = indexes.Select(d => BsonSerializer.Deserialize<IndexDescription>(d))
                               .Where(i => i.Unique)
                               .Where(i => i.Key.Count() == 1)
                               .First(i => i.Key.Contains("Name"));
            Expect(index.Key.Count(), Is.EqualTo(1));
        }
    }
}