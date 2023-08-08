using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab12_AsyncInnManagementSystem.Data;
using Lab12_AsyncInnManagementSystem.Models;

namespace Lab12_AsyncInnManagementSystem.Controllers
{
    public class HotelRoomsController : Controller
    {
        private readonly AsyncInnContext _context;

        public HotelRoomsController(AsyncInnContext context)
        {
            _context = context;
        }

        // GET: HotelRooms
        public async Task<IActionResult> Index()
        {
            var asyncInnContext = _context.HotelRoom.Include(h => h.Hotel).Include(h => h.Room);
            return View(await asyncInnContext.ToListAsync());
        }

        // GET: HotelRooms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.HotelRoom == null)
            {
                return NotFound();
            }

            var hotelRoom = await _context.HotelRoom
                .Include(h => h.Hotel)
                .Include(h => h.Room)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (hotelRoom == null)
            {
                return NotFound();
            }

            return View(hotelRoom);
        }

        // GET: HotelRooms/Create
        public IActionResult Create()
        {
            ViewData["HotelID"] = new SelectList(_context.Hotel, "Id", "Address");
            ViewData["RoomID"] = new SelectList(_context.Room, "ID", "Name");
            return View();
        }

        // POST: HotelRooms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,HotelID,RoomID,Price")] HotelRoom hotelRoom)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hotelRoom);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HotelID"] = new SelectList(_context.Hotel, "Id", "Address", hotelRoom.HotelID);
            ViewData["RoomID"] = new SelectList(_context.Room, "ID", "Name", hotelRoom.RoomID);
            return View(hotelRoom);
        }

        // GET: HotelRooms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.HotelRoom == null)
            {
                return NotFound();
            }

            var hotelRoom = await _context.HotelRoom.FindAsync(id);
            if (hotelRoom == null)
            {
                return NotFound();
            }
            ViewData["HotelID"] = new SelectList(_context.Hotel, "Id", "Address", hotelRoom.HotelID);
            ViewData["RoomID"] = new SelectList(_context.Room, "ID", "Name", hotelRoom.RoomID);
            return View(hotelRoom);
        }

        // POST: HotelRooms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,HotelID,RoomID,Price")] HotelRoom hotelRoom)
        {
            if (id != hotelRoom.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hotelRoom);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HotelRoomExists(hotelRoom.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["HotelID"] = new SelectList(_context.Hotel, "Id", "Address", hotelRoom.HotelID);
            ViewData["RoomID"] = new SelectList(_context.Room, "ID", "Name", hotelRoom.RoomID);
            return View(hotelRoom);
        }

        // GET: HotelRooms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.HotelRoom == null)
            {
                return NotFound();
            }

            var hotelRoom = await _context.HotelRoom
                .Include(h => h.Hotel)
                .Include(h => h.Room)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (hotelRoom == null)
            {
                return NotFound();
            }

            return View(hotelRoom);
        }

        // POST: HotelRooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.HotelRoom == null)
            {
                return Problem("Entity set 'AsyncInnContext.HotelRoom'  is null.");
            }
            var hotelRoom = await _context.HotelRoom.FindAsync(id);
            if (hotelRoom != null)
            {
                _context.HotelRoom.Remove(hotelRoom);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HotelRoomExists(int id)
        {
          return (_context.HotelRoom?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
