using System;

namespace HealthApp.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        // navigation property
        public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    }

}