using Lab12_AsyncInnManagementSystem.Data;
using Lab12_AsyncInnManagementSystem.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab12_AsyncInnManagementSystem.Models.Services
{
    public class HotelService : IHotel
    {
        
        private AsyncInnContext _context;

        public HotelService(AsyncInnContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> DeleteHotel(int id)
        {
            var hotel = await _context.Hotel.FindAsync(id);
            _context.Hotel.Remove(hotel);
            await _context.SaveChangesAsync();

            return null;
        }

        public async Task<ActionResult<IEnumerable<Hotel>>> GetHotel()
        {
            return await _context.Hotels.ToListAsync();
        }

        public async Task<ActionResult<Hotel>> GetHotel(int id)
        {
            return await _context.Hotel.FindAsync(id);
        }

        public bool HotelExists(int id)
        {
            return  (_context.Hotel?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public async Task<ActionResult<Hotel>> PostHotel(Hotel hotel)
        {
            _context.Hotel.Add(hotel);
            await _context.SaveChangesAsync();

            return hotel;
        }

        public async Task<IActionResult> PutHotel(int id, Hotel hotel)
        {
            _context.Entry(hotel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
               
                {
                    throw;
                }
            }
                return null;
        }
    }
}
