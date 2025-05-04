using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using HotelDBLibrary.Models;

public class RoomsController : Controller

{
    private readonly HotelDbContext _context;

    public RoomsController(HotelDbContext context)
    {
        _context = context;
    }

    // GET: Rooms
    public async Task<IActionResult> Index()
    {
        var rooms = await _context.Rooms.ToListAsync();
        return View(model: rooms);
    }

    // GET: Rooms/Book/5
    public async Task<IActionResult> Book(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var room = await _context.Rooms.FindAsync(id);
        if (room == null)
        {
            return NotFound();
        }

        // Get the guest's phone number from session
        var guestTlf = HttpContext.Session.GetString("GuestTlf");
        if (string.IsNullOrEmpty(guestTlf))
        {
            return RedirectToAction("Login", "Account");  // If no guest, redirect to login
        }

        // Create an empty reservation with the room details
        var reservation = new Reservation
        {
            RoomId = room.Id,
            // Optionally, if you need more details, add them here
        };

        return View(reservation);
    }

    // POST: Rooms/Book/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Book(int id, [Bind("GuestId, Start, End, NumberOfGuests")] Reservation reservation)
    {
        if (ModelState.IsValid)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room == null)
            {
                return NotFound();
            }

            reservation.RoomId = room.Id;

            // You might want to add additional logic here to check availability, etc.

            // Add the reservation to the context and save changes
            _context.Add(reservation);
            await _context.SaveChangesAsync();

            return RedirectToAction("ReservationConfirmation", new { id = reservation.Id });
        }

        return View(reservation);
    }

    // GET: ReservationConfirmation/5
    public async Task<IActionResult> ReservationConfirmation(int id)
    {
        var reservation = await _context.Reservations.FindAsync(id);
        if (reservation == null)
        {
            return NotFound();
        }

        // Pass the reservation to the view for display
        return View(reservation);
        
        if (reservation.Start < new DateTime(1753, 1, 1) || reservation.End < new DateTime(1753, 1, 1))
        {
            ModelState.AddModelError("", "Start and End dates must be after 01.01.1753.");
            return View(reservation);
        }

    }
}

