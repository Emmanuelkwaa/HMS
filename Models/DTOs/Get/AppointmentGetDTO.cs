using System;
using System.ComponentModel.DataAnnotations;
using HMS.Models.DTOs.Get;

namespace HMS.Models
{
    public class AppointmentGetDTO
    {
        public int Id { get; set; }
        public DoctorReceptionGetDTO Doctor { get; set; }
        public PatientReceptionGetDTO Patient { get; set; }
        [Required]
        public string Detail { get; set; }
        [Required]
        public DateTime DateOfAppointment { get; set; }
        public TimeSpan DaysUntilAppointment { get; set; }
    }
}