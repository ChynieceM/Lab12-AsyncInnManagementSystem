using Lab12_AsyncInnManagementSystem.Models;
using Microsoft.EntityFrameworkCore; 
namespace Lab12_AsyncInnManagementSystem.Data
{
    public class AsyncInnContext: DbContext
    {
        public DbSet<Amenity> Amenitites;
        public DbSet<RoomAmenity> RoomAmenities;
        public DbSet<Room> Rooms;
        public DbSet<HotelRoom> HotelRooms;
        public DbSet<Hotel> Hotels;

        public AsyncInnContext(DbContextOptions options) : base(options) { }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Amenity>().HasData(new Amenity
        { ID = 1, Name = "A/C" });

        modelBuilder.Entity<Room>().HasData(new Room
        { ID = 1, Layout = 0, Name = "Basic Room" }, new Room { ID = 2, Layout = 0, Name = "Basic Single Room" } );


        modelBuilder.Entity<Hotel>().HasData(new Hotel
        { Id = 1, Name = "Elmo's Hotel", Address = "123 Sesamee Street", 
            City = "Memphis", State = "TN", Phone = "123-345-7890" });

        //lookup tables
        modelBuilder.Entity<RoomAmenity>().HasData(new RoomAmenity
        { ID = 1, AmenityID = 1, RoomID = 1 });
        modelBuilder.Entity<HotelRoom>().HasData(new HotelRoom { ID = 1, HotelID = 1, Price = 100.70, RoomID = 1 });
    }


    public DbSet<Lab12_AsyncInnManagementSystem.Models.Hotel> Hotel { get; set; } = default!;


    public DbSet<Lab12_AsyncInnManagementSystem.Models.Room> Room { get; set; } = default!;


    public DbSet<Lab12_AsyncInnManagementSystem.Models.Amenity> Amenity { get; set; } = default!;


    public DbSet<Lab12_AsyncInnManagementSystem.Models.HotelRoom> HotelRoom { get; set; } = default!;

        public DbSet<Lab12_AsyncInnManagementSystem.Models.RoomAmenity> RoomAmenity { get; set; } = default!;
    }
}
