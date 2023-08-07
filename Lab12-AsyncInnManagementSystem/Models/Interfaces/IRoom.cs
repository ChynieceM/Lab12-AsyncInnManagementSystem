using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab12_AsyncInnManagementSystem.Models.Interfaces
{
    public interface IRoom 
    {
        public Task<ActionResult<IEnumerable<Room>>> GetRoom();

        public Task<ActionResult<Room>> GetRoom(int id);

        public Task<IActionResult> PutRoom(int id, Room room);

        public Task<ActionResult<Room>> PostRoom(Room room);

        public Task<IActionResult> DeleteRoom(int id);


        bool RoomExists(int id);
        
    }


}

