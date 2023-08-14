using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lab12_AsyncInnManagementSystem.Data;
using Lab12_AsyncInnManagementSystem.Models;

namespace Lab12_AsyncInnManagementSystem.Controllers
{
    //https://localhost:2343/api/Hotels
    [Route("api/[controller]")]
    [ApiController]
    public class HotelRoomController : ControllerBase
    {
        private readonly AsyncInnContext _context;

        public HotelRoomController(AsyncInnContext context)
        {
            {
                _context = context;
            }
        }
            // GET: api/HotelRoom
            [HttpGet]
            [Route("/api/HotelRoom")]
            public async Task<ActionResult<IEnumerable<HotelRoom>>> GetHotelRooms()
            {
                if (_context.HotelRooms == null)
                {
                    return NotFound();
                }
                return await _context.HotelRooms.ToListAsync();
            }

            //new route to get all routes
            [HttpGet]
            [Route("/api/HotelRoom/{hotelId}/Rooms")]
            public async Task<ActionResult<IEnumerable<HotelRoom>>> GetAllRoomsForHotelAsync(int hotelId)
            {
                if (hotelId == 0)
                {
                    return NotFound();
                }
                var hotelRoom = await _context.HotelRooms.Where(hr => hr.HotelID == hotelId).ToListAsync();

                return hotelRoom;
                /* List<HotelRoom> rooms = await _context.HotelRooms.Where(hr => hr.HotelID == hotelId).ToListAsync();
                 return Ok(rooms);*/
            }

            // GET: api/HotelRoom/5
            [HttpGet("{id}")]
            public async Task<ActionResult<HotelRoom>> GetHotelRoom(int id)
            {
                if (_context.HotelRooms == null)
                {
                    return NotFound();
                }
                var hotelRoom = await _context.HotelRooms.FindAsync(id);

                if (hotelRoom == null)
                {
                    return NotFound();
                }

                return hotelRoom;
            }

            // PUT: api/HotelRoom/5
            // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
            [HttpPut("{id}")]
            public async Task<IActionResult> PutHotelRoom(int id, HotelRoom hotelRoom)
            {
                if (id != hotelRoom.ID)
                {
                    return BadRequest();
                }

                _context.Entry(hotelRoom).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HotelRoomExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return NoContent();
            }


            ///api/Hotels/{hotelId}/Rooms/{roomNumber}
            [HttpPut]
            [Route("/api/HotelRoom/{hotelId}/Rooms/{roomId}")]
            public async Task<IActionResult> UpdateHotelRoomInHotel(int hotelId, int roomId, [FromBody] HotelRoom updatedHotelRoom)
            {
                // Find the hotel
                var hotel = await _context.Hotels.FindAsync(hotelId);
                if (hotel == null)
                {
                    return NotFound("Hotel not found");
                }

                // Find the room within the hotel
                var room = await _context.HotelRooms.FirstOrDefaultAsync(r => r.RoomID == roomId && r.HotelID == hotelId);
                if (room == null)
                {
                    return NotFound("Room not found");
                }


                //////// Update the room with the new details
                // room.Name = updatedHotelRoom.Name; // Update the properties as needed
                if (room.Name != updatedHotelRoom.Name)
                {
                    room.Name = updatedHotelRoom.Name;
                }
                if (room.Price != updatedHotelRoom.Price)
                {
                    room.Price = updatedHotelRoom.Price;

                }
                _context.Entry(room).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HotelRoomExists(hotelId)) // You may need to modify this method to check both hotel and room IDs
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return NoContent();
            }




            [HttpDelete]
            [Route("/api/Hotels/{hotelId}/Rooms/{roomID}")]
            public async Task<IActionResult> DeleteSpecificRoom(int hotelId, int roomID)
            {
                var hotelRoom = await _context.HotelRooms.FirstOrDefaultAsync(r => r.HotelID == hotelId && r.RoomID == roomID);
                if (hotelRoom == null)
                {
                    return NotFound();
                }

                _context.HotelRooms.Remove(hotelRoom);
                await _context.SaveChangesAsync();

                return NoContent();

            }



            // POST: api/HotelRoom
            // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
            [HttpPost]
            public async Task<ActionResult<HotelRoom>> PostHotelRoom(HotelRoom hotelRoom)
            {
                if (_context.HotelRooms == null)
                {
                    return Problem("Entity set 'AsyncInnContext.HotelRooms'  is null.");
                }
                _context.HotelRooms.Add(hotelRoom);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetHotelRoom", new { id = hotelRoom.ID }, hotelRoom);
            }


            [HttpPost]
            [Route("/api/HotelRoom/{hotelId}/Rooms")]
            public async Task<ActionResult<HotelRoom>> AddRoomToHotel(int hotelID, [FromQuery] int roomID, [FromBody] HotelRoom hotelRoom)
            {
                var hotel = await _context.Hotels.FindAsync(hotelID);
                var room = await _context.Rooms.FindAsync(roomID);

                if (hotel == null)
                {
                    return NotFound($"Hotel with ID {hotelID} not found.");
                }
                else if (room == null)
                {
                    return NotFound();
                }

                //hotelRoom.HotelID = hotelId;

                HotelRoom NewHotelRoom = new HotelRoom() { HotelID = hotel.ID, RoomID = room.ID, Name = hotelRoom.Name, Price = hotelRoom.Price };
                _context.HotelRooms.Add(NewHotelRoom);
                await _context.SaveChangesAsync();

                return CreatedAtAction("AddRoomToHotel", new { id = NewHotelRoom.ID }, NewHotelRoom);

            }


            // DELETE: api/HotelRoom/5
            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteHotelRoom(int id)
            {
                if (_context.HotelRooms == null)
                {
                    return NotFound();
                }
                var hotelRoom = await _context.HotelRooms.FindAsync(id);
                if (hotelRoom == null)
                {
                    return NotFound();
                }

                _context.HotelRooms.Remove(hotelRoom);
                await _context.SaveChangesAsync();

                return NoContent();
            }





            private bool HotelRoomExists(int id)
            {
                return (_context.HotelRooms?.Any(e => e.ID == id)).GetValueOrDefault();
            }
        }
    } 

