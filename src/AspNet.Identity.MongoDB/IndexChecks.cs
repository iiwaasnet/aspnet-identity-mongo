namespace AspNet.Identity.MongoDB
{
    using System.Threading.Tasks;
    using global::MongoDB.Driver;

    public static class IndexChecks
    {
        public static Task EnsureUniqueIndexOnUserName<TUser>(this IMongoCollection<TUser> users)
            where TUser : IdentityUser
        {
            return users.Indexes.CreateAsync(new IndexDefinitionBuilder<TUser>().Ascending(u => u.UserName),
                                      new CreateIndexOptions {Background = true, Unique = true});

            //var userName = new IndexKeysBuilder<IdentityUser>().Ascending(t => t.UserName);
            //var unique = new IndexOptionsBuilder().SetUnique(true);
            //users.CreateIndex(userName, unique);
        }

        public static Task EnsureUniqueIndexOnRoleName<TRole>(this IMongoCollection<TRole> roles)
            where TRole : IdentityRole
        {
            return roles.Indexes.CreateAsync(new IndexDefinitionBuilder<TRole>().Ascending(r => r.Name),
                                      new CreateIndexOptions {Background = true, Unique = true});

            //var roleName = new IndexKeysBuilder<IdentityRole>().Ascending(t => t.Name);
            //var unique = new IndexOptionsBuilder().SetUnique(true);
            //roles.CreateIndex(roleName, unique);
        }

        public static Task EnsureUniqueIndexOnEmail<TUser>(this IMongoCollection<TUser> users)
            where TUser : IdentityUser
        {
            return users.Indexes.CreateAsync(new IndexDefinitionBuilder<TUser>().Ascending(u => u.Email),
                                      new CreateIndexOptions { Background = true, Unique = true });

            //var email = new IndexKeysBuilder<IdentityUser>().Ascending(t => t.Email);
            //var unique = new IndexOptionsBuilder().SetUnique(true);
            //users.CreateIndex(email, unique);
        }
    }
}