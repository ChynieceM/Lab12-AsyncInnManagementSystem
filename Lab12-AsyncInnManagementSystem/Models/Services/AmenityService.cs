using Lab12_AsyncInnManagementSystem.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab12_AsyncInnManagementSystem.Models.Services
{
    public class AmenityService
    {
        private  AsyncInnContext _context;

        public AmenityService(AsyncInnContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<IEnumerable<Amenity>>> GetAmenity()
        {
           
            return await _context.Amenity.ToListAsync();
        }

      
        public async Task<ActionResult<Amenity>> GetAmenity(int id)
        {
          
            var amenity = await _context.Amenity.FindAsync(id);

    
            return amenity;
        }

       
        public async Task<IActionResult> PutAmenity(int id, Amenity amenity)
        {
            

            _context.Entry(amenity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
               
                    throw;
                
            }

            return null;
        }

      
        public async Task<ActionResult<Amenity>> PostAmenity(Amenity amenity)
        {
            
            _context.Amenity.Add(amenity);
            await _context.SaveChangesAsync();

            return amenity;
        }

        
        public async Task<IActionResult> DeleteAmenity(int id)
        {
            
            var amenity = await _context.Amenity.FindAsync(id);
           

            _context.Amenity.Remove(amenity);
            await _context.SaveChangesAsync();

            return null;
        }

        private bool AmenityExists(int id)
        {
            return (_context.Amenity?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }

}
