using Microsoft.EntityFrameworkCore;
using mvp_studio_api;

namespace testApi
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        // TODO: Set Tables

        // public DbSet<UserInfo> Items { get; set; }
    }
}
