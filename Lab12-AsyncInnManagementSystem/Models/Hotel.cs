using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Lab12_AsyncInnManagementSystem.Models
{
    public class Hotel //hotel location
    {

        [Key] 
      public  int Id { get; set; }

        [Required]
      public  string Name { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public  string Phone { get; set; }

        
        //navigation properties 
        public List<HotelRoom> HotelRooms { get; set; }
    }
}
