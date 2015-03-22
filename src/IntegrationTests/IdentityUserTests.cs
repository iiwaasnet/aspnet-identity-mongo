namespace IntegrationTests
{
    using AspNet.Identity.MongoDB;
    using MongoDB.Bson;
    using NUnit.Framework;
    using System.Threading.Tasks;

    [TestFixture]
    public class IdentityUserTests : UserIntegrationTestsBase
    {
        [Test]
        public async Task Insert_NoId_SetsId()
        {
            var user = new IdentityUser();

            await Users.InsertOneAsync(user);

            Expect(user.Id, Is.Not.Null);
            var parsed = ObjectId.Parse(user.Id);
            Expect(parsed, Is.Not.Null);
            Expect(parsed, Is.Not.EqualTo(ObjectId.Empty));
        }
    }
}