using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HMS.Models.DTOs.Get
{
    public class DoctorGetDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string LicenseNumber { get; set; }
        public Department Department { get; set; }
        //public IEnumerable<DateTime> WorkingDays { get; set; }
        public string Specialty { get; set; }
        public string Sex { get; set; }
        public AddressGetDTO Address { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        

        public ICollection<PatientGetDTO> AssignedPatients { get; set; }
    }
}