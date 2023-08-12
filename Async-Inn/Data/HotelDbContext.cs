using Async_Inn.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Async_Inn.Data
{
    public class HotelDbContext :IdentityDbContext<ApplicationUser>
    {
        public HotelDbContext(DbContextOptions option):base(option)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Hotel>().HasData(
                new Hotel() { Id = 1, Name = "cont", StreetAddres = "23.st", City = "NY", State = "NY", Country = "US", Phone = "1233442" },
                new Hotel() { Id = 2, Name = "res", StreetAddres = "25.st", City = "LA", State = "LA", Country = "US", Phone = "1233444442" },
                new Hotel() { Id = 3, Name = "hunt", StreetAddres = "28.st", City = "TX", State = "TX", Country = "US", Phone = "123223442" }
                );
            modelBuilder.Entity<Room>().HasData(
              new Room() { Id = 1, Name = "honey", Layout = 1 },
              new Room() { Id = 2, Name = "red", Layout = 2 },
              new Room() { Id = 3, Name = "white", Layout = 3 }
              );
            modelBuilder.Entity<Amenities>().HasData(
              new Amenities() { Id = 1, Name = "AC" },
              new Amenities() { Id = 2, Name = "coffeeBar" },
              new Amenities() { Id = 3, Name = "Fridge" }
              );

            modelBuilder.Entity<RoomAmeneties>().HasKey(
                roomamanites => new { roomamanites.RoomId, roomamanites.AmenetiesId }
                );
            modelBuilder.Entity<HotelRoom>().HasKey(
               roomamanites => new { roomamanites.HotelId, roomamanites.RoomNumber }
               );
            SeedRole(modelBuilder, "District Manager");
            SeedRole(modelBuilder, "Property Manager");
            SeedRole(modelBuilder, "Agent");
            SeedRole(modelBuilder, "Anonymous users");
        }


        int nextid = 1;
        private void SeedRole(ModelBuilder model,string roleName,params string[] permisstion)
        {
            var roles = new IdentityRole
            {
                Id =roleName.ToLower(),
                Name= roleName,
                NormalizedName= roleName.ToUpper(),
                ConcurrencyStamp =Guid.Empty.ToString()
            };
            model.Entity<IdentityRole>().HasData( roles );
            var RoleClamis = permisstion.Select(permistion => new IdentityRoleClaim<string>
            {
                Id = nextid++,
                RoleId=roles.Id,
                ClaimType="Permisstion",
                ClaimValue=permistion
            });
            model.Entity<IdentityRoleClaim<string>>().HasData( RoleClamis );
        }
        


        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Amenities> Amenities { get; set; }
        public DbSet<RoomAmeneties> RoomAmeneties { get; set; }

        public DbSet<HotelRoom> HotelRooms { get; set;}

    }
}
