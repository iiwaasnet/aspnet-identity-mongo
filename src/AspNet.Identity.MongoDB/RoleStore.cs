namespace AspNet.Identity.MongoDB
{
    using System.Threading.Tasks;
    using global::MongoDB.Driver;
    using Microsoft.AspNet.Identity;

    /// <summary>
    ///     Note: Deleting and updating do not modify the roles stored on a user document. If you desire this dynamic
    ///     capability, override the appropriate operations on RoleStore as desired for your application. For example you could
    ///     perform a document modification on the users collection before a delete or a rename.
    /// </summary>
    /// <typeparam name="TRole"></typeparam>
    public class RoleStore<TRole> : IRoleStore<TRole>
        //IQueryableRoleStore<TRole>
        where TRole : IdentityRole
    {
        private readonly RolesContext<TRole> context;

        public RoleStore(RolesContext<TRole> context)
        {
            this.context = context;
        }

        //public virtual IQueryable<TRole> Roles
        //{
        //    get { return context.Roles.AsQueryable<TRole>(); }
        //}

        public virtual void Dispose()
        {
            // no need to dispose of anything, mongodb handles connection pooling automatically
        }

        public virtual Task CreateAsync(TRole role)
        {
            // TODO: Unique index on role name

            return context.Roles.InsertOneAsync(role);

            //return Task.Run(() => context.Roles.Insert(role));
        }

        public virtual Task UpdateAsync(TRole role)
        {
            // TODO: Unique index on role name
            return context.Roles.FindOneAndUpdateAsync(r => r.Id == role.Id,
                                                       new ObjectUpdateDefinition<TRole>(role));
            //return Task.Run(() => context.Roles.Save(role));
        }

        public virtual Task DeleteAsync(TRole role)
        {
            return context.Roles.FindOneAndDeleteAsync(r => r.Id == role.Id);

            //var queryById = Query<TRole>.EQ(r => r.Id, role.Id);
            //return Task.Run(() => context.Roles.Remove(queryById));
        }

        public virtual Task<TRole> FindByIdAsync(string roleId)
        {
            return context.Roles.Find(r => r.Id == roleId).FirstOrDefaultAsync();

            //return Task.Run(() => context.Roles.FindOneByIdAs<TRole>(ObjectId.Parse(roleId)));
        }

        public virtual Task<TRole> FindByNameAsync(string roleName)
        {
            return context.Roles.Find(r => r.Name == roleName).SingleOrDefaultAsync();

            //var queryByName = Query<TRole>.EQ(r => r.Name, roleName);
            //return Task.Run(() => context.Roles.FindOneAs<TRole>(queryByName));
        }
    }
}