using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HMS.Models.DTOs.Get
{
    public class PatientGetDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Sex { get; set; }
        public decimal Weight { get; set; }
        public string Height { get; set; }
        public string BloodType { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Nationality { get; set; }
        public EmergencyContact EmergencyContact { get; set; }
        public Address Address { get; set; }
        public Room Room { get; set; }
        public PatientDoctorUpdateDTO Doctor { get; set; }
    }
}