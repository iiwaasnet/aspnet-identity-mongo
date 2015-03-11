namespace AspNet.Identity.MongoDB
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using global::MongoDB.Driver;

    public class UsersContext<TUser>
        where TUser : IdentityUser
    {
        public UsersContext(IMongoCollection<TUser> users)
        {
            Users = users;
        }

        public IEnumerable<Task> EnsureUniqueIndexes()
        {
            var tasks = new List<Task>
                        {
                            Users.EnsureUniqueIndexOnEmail(),
                            Users.EnsureUniqueIndexOnUserName()
                        };

            return tasks;
        }

        public IMongoCollection<TUser> Users { get; private set; }
    }
}