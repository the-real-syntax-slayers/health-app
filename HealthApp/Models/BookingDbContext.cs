using Microsoft.EntityFrameworkCore;

namespace HealthApp.Models;

public class BookingDbContext : DbContext
{
    public BookingDbContext(DbContextOptions<BookingDbContext> options) : base(options)
    {
        // Database.EnsureCreated();
    }

    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Employee> Employees { get; set; }

}