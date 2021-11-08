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
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime DateAdmitted { get; set; }
        public DateTime DateDischarge { get; set; }
        public Address Address { get; set; }
        public Room Room { get; set; }
    }
}