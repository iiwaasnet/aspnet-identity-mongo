namespace AspNet.Identity.MongoDB
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using global::MongoDB.Driver;

    public class RolesContext<TRole>
        where TRole : IdentityRole
    {
        public RolesContext(IMongoCollection<TRole> roles)
        {
            Roles = roles;            
        }

        public IEnumerable<Task> EnsureUniqueIndexes()
        {
            var task = Roles.EnsureUniqueIndexOnRoleName();

            return new[] {task};
        }

        public IMongoCollection<TRole> Roles { get; private set; }
    }
}