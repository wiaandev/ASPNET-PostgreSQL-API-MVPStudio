using Microsoft.EntityFrameworkCore;
using mvp_studio_api;
using mvp_studio_api.Models;

namespace testApi
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Admin> Admin { get; set; }
        public DbSet<Client> Client { get; set; }
        public DbSet<ClientType> Client_Type { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Fund> Fund { get; set; }
        public DbSet<Project> Project { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Team> Team { get; set; }

        public DbSet<ProjectAssigned> ProjectAssigned { get; set; } 
    }
}
