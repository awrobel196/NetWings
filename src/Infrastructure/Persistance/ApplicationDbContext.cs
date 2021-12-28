using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<EnvVariable> EnvVariable { get; set; }
        public DbSet<WebsiteResponseScore> WebsiteResponseScore { get; set; }
        public DbSet<License> License { get; set; }
        public DbSet<LicenseType> LicenseType { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Website> Website { get; set; }
        public DbSet<WebsiteResponseSettings> WebsiteResponseSettings { get; set; }
        public DbSet<MachineResponseSettings> MachineResponseSettings { get; set; }
        public DbSet<MachineResetSettings> MachineResetSettings { get; set; }
        public DbSet<MachineUptimeSettings> MachineUptimeSettings { get; set; }
        public DbSet<WebsiteUptimeMachineLibrary> WebsiteUptimeMachineLibrary { get; set; }
        public DbSet<MachineUptimeLog> MachineUptimeLog { get; set; }
        public DbSet<MachineResponseLog> MachineResponseLog { get; set; }
        public DbSet<WebsiteUptimeScore> WebsiteUptimeScore { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property<string>("password").HasColumnName("Password").IsRequired();
            });
        }
    }
}

