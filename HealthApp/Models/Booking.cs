using System;
using System.ComponentModel.DataAnnotations;

namespace HealthApp.Models
{
    public class Booking
    {
        public int BookingId { get; set; }

        public string? Description { get; set; }

        public DateTime Date { get; set; }

        public int PatientId { get; set; }

        // navigation property
        public virtual Patient Patient { get; set; } = default!;

        public int EmployeeId { get; set; }

        // navigation property
        public virtual Employee Employee { get; set; } = default!;

    }

}