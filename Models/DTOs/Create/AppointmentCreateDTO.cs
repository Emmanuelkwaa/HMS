using System;
using System.ComponentModel.DataAnnotations;
using HMS.Models.DTOs.Get;

namespace HMS.Models
{
    public class AppointmentCreateDTO
    {
        public int Id { get; set; }
        [Required]
        public string Detail { get; set; }
        [Required]
        public DateTime DateOfAppointment { get; set; }
    }
}