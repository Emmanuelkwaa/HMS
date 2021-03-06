using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HMS.Models.DTOs.Get;

namespace HMS.Models.DTOs.Create
{
    public class PatientUpdateDTO
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; } 
        public string Sex { get; set; }
        public decimal Weight { get; set; }
        public string Height { get; set; }
        public string BloodType { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Nationality { get; set; }

        public EmergencyContact EmergencyContact { get; set; }
        public Address Address { get; set; }
        public Room Room { get; set; }
    }
}