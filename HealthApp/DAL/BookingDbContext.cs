using Microsoft.EntityFrameworkCore;
using HealthApp.Models;

namespace HealthApp.DAL;

public class BookingDbContext : DbContext
{
    public BookingDbContext(DbContextOptions<BookingDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Employee> Employees { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();
    }

}