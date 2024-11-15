using CafeService.Api.Entities;
using CafeService.Api.Enums;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using static CafeService.Api.Enums.CommonResources;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CafeService.Api.Context
{
    public class AppDbContext : DbContext
    {
        //public AppDbContext()
        //{

        //}


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
             ChangeTracker.LazyLoadingEnabled = true;          
        }
        public DbSet<Cafe> Cafes { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {



            #region enum configurations
            //builder.Entity<CommonResources>().Property(e => e.Gender).HasConversion<int>();
            #endregion

            builder.Entity<Employee>()
          .HasOne(o => o.Cafe)
          .WithMany(c => c.Employees)
          .HasForeignKey(o => o.FK_CafeId)
          .IsRequired(false); // Nullable foreign key



            base.OnModelCreating(builder);

            builder.Entity<Cafe>().HasData(
               new Cafe
               {
                   Id = Guid.NewGuid(),
                   Name = "Central Perk",
                   Description = "A cozy place to enjoy your coffee.",
                   Logo = null,
                   Location = "New York, NY",
                   CreatedDateTime = DateTime.UtcNow,
                   ModifiedDateTime = DateTime.UtcNow
               },
               new Cafe
               {
                   Id = Guid.NewGuid(),
                   Name = "Blue Bottle Coffee",
                   Description = "Specialty coffee roaster and retailer.",
                   Logo = null,
                   Location = "San Francisco, CA",
                   CreatedDateTime = DateTime.UtcNow,
                   ModifiedDateTime = DateTime.UtcNow
               },
               new Cafe
               {
                   Id = Guid.NewGuid(),
                   Name = "Cafe Nero",
                   Description = "European style coffee house.",
                   Logo = null,
                   Location = "London, UK",
                   CreatedDateTime = DateTime.UtcNow,
                   ModifiedDateTime = DateTime.UtcNow
               }
           );

            builder.Entity<Employee>().HasData(
            new Employee
            {
               
                Id = "UIA123456",
                Name = "Alice Johnson",
                EmailAddress = "alice.johnson@example.com",
                PhoneNumber = "91234567",
                Gender = CommonResources.Gender.Female,
                FK_CafeId=null,
                CreatedDateTime = DateTime.UtcNow,
                ModifiedDateTime = DateTime.UtcNow

            },
            new Employee
            {
               
                Id = "UIB987654",
                Name = "Bob Smith",
                EmailAddress = "bob.smith@example.com",
                PhoneNumber = "81234567",
                Gender = CommonResources.Gender.Male,
                 FK_CafeId = null,
                  CreatedDateTime = DateTime.UtcNow,
                ModifiedDateTime = DateTime.UtcNow
            },
            new Employee
            {

                Id = "UIC765432",
                Name = "Charlie Brown",
                EmailAddress = "charlie.brown@example.com",
                PhoneNumber = "92345678",
                Gender = CommonResources.Gender.Male,
                 FK_CafeId = null,
                  CreatedDateTime = DateTime.UtcNow,
                ModifiedDateTime = DateTime.UtcNow
            },
            new Employee
            {

                Id = "UID456789",
                Name = "Diana Prince",
                EmailAddress = "diana.prince@example.com",
                PhoneNumber = "83456789",
                Gender = CommonResources.Gender.Female,
                FK_CafeId = null,
                 CreatedDateTime = DateTime.UtcNow,
                ModifiedDateTime = DateTime.UtcNow
            }
        );

        }
    }
}
