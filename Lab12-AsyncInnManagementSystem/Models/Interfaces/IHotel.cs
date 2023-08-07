using Microsoft.AspNetCore.Mvc;

namespace Lab12_AsyncInnManagementSystem.Models.Interfaces
{
    public interface IHotel
    {
       public Task<ActionResult<IEnumerable<Hotel>>> GetHotel();

       public Task<ActionResult<Hotel>> GetHotel(int id);

       public Task<IActionResult> PutHotel(int id, Hotel hotel);


       public Task<ActionResult<Hotel>> PostHotel(Hotel hotel);

       public  Task<IActionResult> DeleteHotel(int id);

        bool HotelExists(int id);

    }
}
