using HealthApp.Models;
using HealthApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HealthApp.DAL;

namespace HealthApp.Controllers;

public class BookingController : Controller
{

    private readonly IBookingRepository _bookingRepository;

    public BookingController(IBookingRepository bookingRepository)
    {
        _bookingRepository = bookingRepository;
    }

    public async Task<IActionResult> Calendar()
    {
        // List<Booking> 
        var bookings = await _bookingRepository.GetAll();
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
            // _bookingDbContext.Bookings.Add(booking);
            // await _bookingDbContext.SaveChangesAsync();
            await _bookingRepository.Create(booking);
            return RedirectToAction(nameof(Calendar));
        }
        Console.WriteLine("Return 2");
        return View(booking);
    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var booking = await _bookingRepository.GetItemById(id);
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
            await _bookingRepository.Update(booking);
            return RedirectToAction(nameof(Calendar));
        }
        return View(booking);
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var booking = await _bookingRepository.GetItemById(id);
        if (booking == null)
        {
            return NotFound();
        }
        return View(booking);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var booking = await _bookingRepository.Delete(id);
        return RedirectToAction(nameof(Calendar));
    }
}