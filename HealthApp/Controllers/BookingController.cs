using HealthApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace HealthApp.Controllers;

public class BookingController : Controller
{
    public IActionResult Calendar()
    {
        var bookings = GetBookings();
        ViewBag.CurrentViewName = "Calendar";
        return View(bookings);
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