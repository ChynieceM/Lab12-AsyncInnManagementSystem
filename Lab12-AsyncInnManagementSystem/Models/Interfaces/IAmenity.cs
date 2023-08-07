using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab12_AsyncInnManagementSystem.Models.Interfaces
{
    public interface IAmenity
    {

        public Task<ActionResult<IEnumerable<Amenity>>> GetAmenity();


        public Task<ActionResult<Amenity>> GetAmenity(int id);


        public  Task<IActionResult> PutAmenity(int id, Amenity amenity);

        public Task<ActionResult<Amenity>> PostAmenity(Amenity amenity);

        public Task<IActionResult> DeleteAmenity(int id);

        bool AmenityExists(int id);
        
    }
}
