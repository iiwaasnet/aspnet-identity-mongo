namespace AspNet.Identity.MongoDB
{
    using global::MongoDB.Driver;

    public static class IndexChecks
    {
        public static void EnsureUniqueIndexOnUserName<TUser>(this IMongoCollection<TUser> users)
            where TUser : IdentityUser
        {
            users.Indexes.CreateAsync(new IndexDefinitionBuilder<TUser>().Ascending(u => u.UserName),
                                      new CreateIndexOptions {Background = true, Unique = true});

            //var userName = new IndexKeysBuilder<IdentityUser>().Ascending(t => t.UserName);
            //var unique = new IndexOptionsBuilder().SetUnique(true);
            //users.CreateIndex(userName, unique);
        }

        public static void EnsureUniqueIndexOnRoleName<TRole>(this IMongoCollection<TRole> roles)
            where TRole : IdentityRole
        {
            roles.Indexes.CreateAsync(new IndexDefinitionBuilder<TRole>().Ascending(r => r.Name),
                                      new CreateIndexOptions {Background = true, Unique = true});

            //var roleName = new IndexKeysBuilder<IdentityRole>().Ascending(t => t.Name);
            //var unique = new IndexOptionsBuilder().SetUnique(true);
            //roles.CreateIndex(roleName, unique);
        }

        public static void EnsureUniqueIndexOnEmail<TUser>(this IMongoCollection<TUser> users)
            where TUser : IdentityUser
        {
            users.Indexes.CreateAsync(new IndexDefinitionBuilder<TUser>().Ascending(u => u.Email),
                                      new CreateIndexOptions { Background = true, Unique = true });

            //var email = new IndexKeysBuilder<IdentityUser>().Ascending(t => t.Email);
            //var unique = new IndexOptionsBuilder().SetUnique(true);
            //users.CreateIndex(email, unique);
        }
    }
}