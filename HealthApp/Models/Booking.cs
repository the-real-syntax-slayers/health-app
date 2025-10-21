using System;

namespace HealthApp.Models
{
    public class Booking
    {
        public int BookingId { get; set; }
        public string? Description { get; set; }
        public DateTime Date { get; set; }

        public int PatientId { get; set; }
        public Patient Patient { get; set; } = default!;


    }

}