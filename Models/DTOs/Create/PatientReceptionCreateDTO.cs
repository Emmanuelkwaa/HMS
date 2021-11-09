using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HMS.Models.DTOs.Get
{
    public class PatientReceptionCreateDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Sex { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}