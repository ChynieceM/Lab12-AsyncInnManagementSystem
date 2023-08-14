using Lab12_AsyncInnManagementSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore; 
namespace Lab12_AsyncInnManagementSystem.Data
{
    public class AsyncInnContext: DbContext
    {
        public DbSet<Amenity> Amenitites { get; set; }
        public DbSet<RoomAmenity> RoomAmenities { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<HotelRoom> HotelRooms { get; set; }
        public DbSet<Hotel> Hotels { get; set; }

        public AsyncInnContext(DbContextOptions options) : base(options) { }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Amenity>().HasData(new Amenity
        { ID = 1, Name = "A/C" });

        modelBuilder.Entity<Room>().HasData(new Room
        { ID = 1, Layout = 0, Name = "Basic Room" }, new Room { ID = 2, Layout = 0, Name = "Basic Single Room" } );


        modelBuilder.Entity<Hotel>().HasData(new Hotel
        { ID = 1, Name = "Elmo's Hotel", Address = "123 Sesamee Street", 
            City = "Memphis", State = "TN", Phone = "123-345-7890" });

        //lookup tables
        modelBuilder.Entity<RoomAmenity>().HasData(new RoomAmenity
        { ID = 1, AmenityID = 1, RoomID = 1 });
        modelBuilder.Entity<HotelRoom>().HasData(new HotelRoom { ID = 1, HotelID = 1, Name = "Chyniece's Room", Price = 100.70, RoomID = 1 });

            SeedRole(modelBuilder, "Admin", "create", "update", "delete");
            SeedRole(modelBuilder, "Editor", "create", "update");
        }

    public DbSet<Lab12_AsyncInnManagementSystem.Models.Hotel> Hotel { get; set; } = default!;


    public DbSet<Lab12_AsyncInnManagementSystem.Models.Room> Room { get; set; } = default!;


    public DbSet<Lab12_AsyncInnManagementSystem.Models.Amenity> Amenity { get; set; } = default!;


    public DbSet<Lab12_AsyncInnManagementSystem.Models.HotelRoom> HotelRoom { get; set; } = default!;

     public DbSet<Lab12_AsyncInnManagementSystem.Models.RoomAmenity> RoomAmenity { get; set; } = default!;
       
        
        //Add to ApplicationUser or a custom DTO class
        public string? Token { get; set; }
        public IList<string>? Roles { get; set; }

        //add underneath the OnModelCreating method in your context file
        private int nextId = 1;

        private void SeedRole(ModelBuilder modelBuilder, string roleName, params string[] permissions)
        {
            var role = new IdentityRole
            {
                Id = roleName.ToLower(),
                Name = roleName,
                NormalizedName = roleName.ToUpper(),
                ConcurrencyStamp = Guid.Empty.ToString()
            };

            modelBuilder.Entity<IdentityRole>().HasData(role);

            // Go through the permissions list (the params) and seed a new entry for each
            var roleClaims = permissions.Select(permission =>
              new IdentityRoleClaim<string>
              {
                  Id = nextId++,
                  RoleId = role.Id,
                  ClaimType = "permissions", // This matches what we did in Startup.cs
                  ClaimValue = permission
              }).ToArray();

            modelBuilder.Entity<IdentityRoleClaim<string>>().HasData(roleClaims);
        }
    }
}
