using System;
using System.ComponentModel.DataAnnotations;

namespace HMS.Models
{
    public class Appointment
    {
        public String Id { get; set; }
        public Doctor Doctor { get; set; }
        public Patient Patient { get; set; }
        [Required]
        public string Detail { get; set; }
        [Required]
        public DateTime DateOfAppointment { get; set; }
        public TimeSpan DaysUntilAppointment { get; set; }
    }
}