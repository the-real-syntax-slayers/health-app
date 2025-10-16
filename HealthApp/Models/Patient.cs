using System;

namespace HealthApp.Models
{
    public class Patient
    {
        public int PatientId { get; set; }
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }
    }

}

