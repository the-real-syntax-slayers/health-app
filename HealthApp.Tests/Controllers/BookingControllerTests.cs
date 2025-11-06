using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using HealthApp.Controllers;
using HealthApp.DAL;
using HealthApp.Models;
using HealthApp.ViewModels;

namespace HealthApp.Test.Controllers;

public class BookingControllerTests
{
    [Fact]
    public async Task TestCalendar()
    {
        // arrange
        var bookingList = new List<Booking>()
        {
            new Booking
            {
                BookingId = 1,
                Description = "Vondt i maven",
                Date = new DateTime(2025, 11, 12, 15, 02, 00),
                PatientId = 1,
                EmployeeId = 1
            }
        };

        var mockBookingRepository = new Mock<IBookingRepository>();
        mockBookingRepository
            .Setup(repo => repo.GetBookingsByMonthAsync(2025, 11))
            .ReturnsAsync(bookingList);
        var mockLogger = new Mock<ILogger<BookingController>>();
        var bookingController = new BookingController(mockBookingRepository.Object, mockLogger.Object);

        // act
        var result = await bookingController.Calendar(2025, 11);

        // assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var bookingsViewModel = Assert.IsAssignableFrom<BookingsViewModel>(viewResult.ViewData.Model);
        Assert.Equal(1, bookingsViewModel.Bookings.Count());
        Assert.Equal(bookingList, bookingsViewModel.Bookings);
    }
}