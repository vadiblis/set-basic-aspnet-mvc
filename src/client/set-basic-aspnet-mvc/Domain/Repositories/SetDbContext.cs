using System.Data.Entity;

using set_basic_aspnet_mvc.Domain.Entities;

namespace set_basic_aspnet_mvc.Domain.Repositories
{
    public class SetDbContext : DbContext
    {
        public SetDbContext(string connectionStringOrName)
            : base(connectionStringOrName)
        {
            Database.SetInitializer(new SetDbInitializer());
        }

        public SetDbContext()
            : this("Name=Set")
        {

        }

        public DbSet<User> Users { get; set; }
    }
}