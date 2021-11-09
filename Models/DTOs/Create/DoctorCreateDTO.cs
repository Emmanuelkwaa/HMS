using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HMS.Models.DTOs.Create
{
    public class DoctorCreateDTO
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string LicenseNumber { get; set; }
        public Department Department { get; set; }
        public string Specialty { get; set; }
        [Required]
        public string Sex { get; set; }
        public int AddressId { get; set; }
        [ForeignKey("AddressId")]
        public Address Address { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
