using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

public class RoomController : Controller
{
    private readonly HotelDbContext _context;

    public RoomController(HotelDbContext context)
    {
        _context = context;
    }

    // GET: Rooms
    public async Task<IActionResult> Index()
    {
        // Fetch only available rooms
        var rooms = await _context.Rooms.Where(r => r.IsAvailable).ToListAsync();
    
        // Pass the list of available rooms to the view
        return View(rooms);
    }


    // GET: Rooms/Book/5
    public async Task<IActionResult> Book(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var room = await _context.Rooms.FindAsync(id);
        if (room == null || !room.IsAvailable)
        {
            return NotFound();
        }

        return View(room);
    }

    // POST: Rooms/Book/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Book(int id, [Bind("CustomerName, StartDate, EndDate")] Reservation reservation)
    {
        if (ModelState.IsValid)
        {
            var room = await _context.Rooms.FindAsync(id);
            reservation.RoomId = room.RoomId;
            room.IsAvailable = false;

            _context.Add(reservation);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        return View(reservation);
    }
}