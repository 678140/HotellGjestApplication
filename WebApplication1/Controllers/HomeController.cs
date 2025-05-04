using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

public class HomeController : Controller
{
    private readonly HotelDbContext _context;

    public HomeController(HotelDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var rooms = await _context.Rooms.Where(r => r.IsAvailable).ToListAsync();
        return View(rooms); // ✅ You must pass the model here
    }
}