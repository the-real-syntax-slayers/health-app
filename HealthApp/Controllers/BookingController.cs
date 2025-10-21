using HealthApp.Models;
using Microsoft.AspNetCore.Mvc;

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