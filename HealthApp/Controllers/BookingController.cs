using HealthApp.Models;
using HealthApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HealthApp.DAL;
using System.Linq;

namespace HealthApp.Controllers;

public class BookingController : Controller
{

    private readonly IBookingRepository _bookingRepository;

    public BookingController(IBookingRepository bookingRepository)
    {
        _bookingRepository = bookingRepository;
    }

    public async Task<IActionResult> Calendar(int? year, int? month)
    {
        DateTime targetDate;
        //Sjekker om en spesifikk dato var skrevet inn i url-en
        if (year.HasValue && month.HasValue)
        {
            targetDate = new DateTime(year.Value, month.Value, 1);
        }
        else
        {
            targetDate = DateTime.Today;
        }

        // 2. Pass the 1st day of that month to the View.
        // The View will use this for its calendar grid logic.
        DateTime firstDayOfTargetMonth = new DateTime(targetDate.Year, targetDate.Month, 1);
        ViewBag.CalendarDate = firstDayOfTargetMonth;
        ViewBag.CurrentViewName = "Calendar"; // You were doing this via the ViewModel, but we can do it here


        // Instead of getting ALL bookings, we get only the ones for the target month.
        var filteredBookings = await _bookingRepository.GetBookingsByMonthAsync(targetDate.Year, targetDate.Month);
    
        /*
        // 3. Get ALL bookings from the repository
        //    (This is what your code did before)
        var allBookings = await _bookingRepository.GetAll();

        // 4. Filter the bookings for ONLY the target month.
        //    This is much more efficient than sending all bookings to the view.
        var filteredBookings = allBookings
            .Where(b => b.Date.Year == targetDate.Year && b.Date.Month == targetDate.Month)
            .ToList();
        */
        // 5. Create the ViewModel using the *filtered* list of bookings
        var bookingsViewModel = new BookingsViewModel(filteredBookings, "Calendar");
        
        return View(bookingsViewModel);


        /*
        // List<Booking> 
        var bookings = await _bookingRepository.GetAll();
        var bookingsViewModel = new BookingsViewModel(bookings, "Calendar");
        return View(bookingsViewModel);
        */
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