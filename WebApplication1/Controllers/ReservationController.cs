using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelDBLibrary.Models;
using System.Linq;
using System.Threading.Tasks;

public class ReservationsController : Controller
{
    private readonly HotelDbContext _context;

    public ReservationsController(HotelDbContext context)
    {
        _context = context;
    }

    // GET: Reservations
    public async Task<IActionResult> MyReservations()
    {
        // Get the guest's phone number from session
        var guestTlf = HttpContext.Session.GetString("GuestTlf");

        if (string.IsNullOrEmpty(guestTlf))
        {
            return RedirectToAction("Login", "Account");  // If no guest is logged in, redirect to login
        }

        // Get the list of reservations made by this guest (filter by Tlf)
        var reservations = await _context.Reservations
            .Where(r => r.Guest.Tlf == guestTlf)  // Filter by guest's phone number
            .Include(r => r.Room)  // Optionally include room details
            .ToListAsync();

        return View(reservations);  // Pass the reservations to the view
    }
}