namespace AspNet.Identity.MongoDB
{
    using global::MongoDB.Driver;

    public class UsersContext<TUser>
    {
        public UsersContext(IMongoCollection<TUser> users)
        {
            Users = users;
        }

        public IMongoCollection<TUser> Users { get; private set; }
    }
}