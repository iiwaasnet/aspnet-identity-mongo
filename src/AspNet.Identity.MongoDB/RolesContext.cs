namespace AspNet.Identity.MongoDB
{
    using global::MongoDB.Driver;

    public class RolesContext<TRole>
    {
        public RolesContext(IMongoCollection<TRole> roles)
        {
            Roles = roles;
        }

        public IMongoCollection<TRole> Roles { get; private set; }
    }
}