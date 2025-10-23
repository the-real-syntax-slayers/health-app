using HealthApp.Models;
using HealthApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HealthApp.Controllers;

public class BookingController : Controller
{

    private readonly BookingDbContext _bookingDbContext;

    public BookingController(BookingDbContext bookingDbContext)
    {
        _bookingDbContext = bookingDbContext;
    }

    public async Task<IActionResult> Calendar()
    {
        List<Booking> bookings = await _bookingDbContext.Bookings.ToListAsync();
        var bookingsViewModel = new BookingsViewModel(bookings, "Calendar");
        return View(bookingsViewModel);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Booking booking)
    {
        Console.WriteLine(booking.BookingId);
        Console.WriteLine(booking.Date);
        Console.WriteLine(booking.Description);
        Console.WriteLine(booking.PatientId);
        Console.WriteLine(booking.Patient);
        if (!ModelState.IsValid)
        {
            _bookingDbContext.Bookings.Add(booking);
            await _bookingDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Calendar));
        }
        Console.WriteLine("Return 2");
        return View(booking);
    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var booking = await _bookingDbContext.Bookings.FindAsync(id);
        if (booking == null)
        {
            return NotFound();
        }
        return View(booking);
    }

    [HttpPost]
    public async Task<IActionResult> Update(Booking booking)
    {
        if (!ModelState.IsValid)
        {
            _bookingDbContext.Bookings.Update(booking);
            await _bookingDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Calendar));
        }
        return View(booking);
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var booking = await _bookingDbContext.Bookings.FindAsync(id);
        if (booking == null)
        {
            return NotFound();
        }
        return View(booking);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var booking = await _bookingDbContext.Bookings.FindAsync(id);
        if (booking == null)
        {
            return NotFound();
        }
        _bookingDbContext.Bookings.Remove(booking);
        await _bookingDbContext.SaveChangesAsync();
        return RedirectToAction(nameof(Calendar));
    }
}