using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class OvertimeDbContext : DbContext
    {
        public OvertimeDbContext(DbContextOptions<OvertimeDbContext> options) : base(options)
        {
        }

        // Add Models to migrate
        public DbSet<Account> Accounts { get; set; }

        public DbSet<AccountRole> AccountRoles { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Overtime> Overtimes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasIndex(e => e.Nik).IsUnique(); // Menambahkan constraint UNIQUE untuk NIK
                entity.HasIndex(e => e.Email).IsUnique(); // Menambahkan constraint UNIQUE untuk Email
                entity.HasIndex(e => e.PhoneNumber).IsUnique(); // Menambahkan constraint UNIQUE untuk PhoneNumber
            });

            // One Employee has one Account
            modelBuilder.Entity<Employee>()
                .HasOne(a => a.Account)
                .WithOne(e => e.Employee)
                .HasForeignKey<Account>(a => a.Guid);

            // One Employee has many Overtime
            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Overtime)
                .WithOne(o => o.Employee)
                .HasForeignKey(o => o.EmployeeGuid);

            // One Employee has one Manager
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Manager)
                .WithMany(m => m.Subordinates)
                .HasForeignKey(e => e.ManagerGuid)
                .OnDelete(DeleteBehavior.Restrict);

            // One Account has many Account Roles
            modelBuilder.Entity<Account>()
                .HasMany(a => a.AccountRoles)
                .WithOne(a => a.Account)
                .HasForeignKey(a => a.AccountGuid);

            // One Role has many Account Roles
            modelBuilder.Entity<Role>()
                .HasMany(a => a.AccountRoles)
                .WithOne(r => r.Role)
                .HasForeignKey(a => a.RoleGuid);
        }
    }
}