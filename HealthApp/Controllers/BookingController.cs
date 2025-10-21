using HealthApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace HealthApp.Controllers;

public class BookingController : Controller
{

    private readonly BookingDbContext _bookingDbContext;

    public BookingController(BookingDbContext bookingDbContext)
    {
        _bookingDbContext = bookingDbContext;
    }
    public IActionResult Calendar()
    {
        List<Booking> bookings = _bookingDbContext.Bookings.ToList();
        ViewBag.CurrentViewName = "Calendar";
        return View(bookings);

        //  var bookingsViewModel = new BookingsViewModel(bookings, "Calendar"); denne linja fungerer ikke før vi
        // har lagd ViewModel klassen
        //var bookings = GetBookings();
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Booking booking)
    {
        if (ModelState.IsValid)
        {
            _bookingDbContext.Bookings.Add(booking);
            _bookingDbContext.SaveChanges();
            return RedirectToAction(nameof(Table));
        }
        return View(booking);
    }

    public List<Booking> GetBookings()
    {
        var bookings = new List<Booking>();
        var booking1 = new Booking
        {
            BookingId = 1,
            Date = new DateTime(2025, 1, 1, 10, 30, 0), // Year, Month, Day, Hour, Minute, Second
        };
        bookings.Add(booking1);
        return bookings;
    }
}