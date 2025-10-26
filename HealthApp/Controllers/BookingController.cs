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
    private readonly ILogger<BookingController> _logger;

    public BookingController(IBookingRepository bookingRepository,
    ILogger<BookingController> logger)
    {
        _bookingRepository = bookingRepository;
        _logger = logger;
    }

    public async Task<IActionResult> Calendar(int? year, int? month)
    {
        _logger.LogInformation("This is an information messeage");
        _logger.LogWarning("This is a warning message");
        _logger.LogError("This is an error message");
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

        if (filteredBookings == null)
        {
            _logger.LogError("[BookingController] Booking list not found while executin _bookingRepository.GetAll()");
            return NotFound("Booking list not found");
        }
        // 5. Create the ViewModel using the *filtered* list of bookings
        var bookingsViewModel = new BookingsViewModel(filteredBookings, "Calendar");

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

        if (!ModelState.IsValid)
        {
            // _bookingDbContext.Bookings.Add(booking);
            // await _bookingDbContext.SaveChangesAsync();
            bool returnOk = await _bookingRepository.Create(booking);
            if (returnOk)
                return RedirectToAction(nameof(Calendar));

        }
        _logger.LogWarning("[BookingController] Booking creation failed {@booking}", booking);
        return View(booking);
    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var booking = await _bookingRepository.GetItemById(id);
        if (booking == null)
        {
            _logger.LogError("[BookingController] Booking not found when updating the BookingId {BookingId:0000}",
            id);
            return BadRequest("Booking not found for the BookingId");
        }
        return View(booking);
    }

    [HttpPost]
    public async Task<IActionResult> Update(Booking booking)
    {
        if (!ModelState.IsValid)
        {
            bool returnOk = await _bookingRepository.Update(booking);
            if (returnOk)
                return RedirectToAction(nameof(Calendar));
        }
        _logger.LogWarning("[BookingController] Booking update failed {@booking}", booking);
        return View(booking);
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var booking = await _bookingRepository.GetItemById(id);
        if (booking == null)
        {
            _logger.LogError("[BookingController] Booking not found for the BookingId {BookingId:0000}",
            id);
            return BadRequest("Booking not found for the BookingId");
        }
        return View(booking);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        bool returnOk = await _bookingRepository.Delete(id);
        if (!returnOk)
        {
            _logger.LogError("[BookingController] Booking deletion failed for the BookingId {BookingId:0000}",
            id);
            return BadRequest("Booking deletion failed");
        }
        return RedirectToAction(nameof(Calendar));
    }
}