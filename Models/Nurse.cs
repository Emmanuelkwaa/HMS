using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HMS.Models
{
    public class Nurse
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
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

        // public int PatientId { get; set; }
        // [ForeignKey("PatientId")]
        // public ICollection<Patient> AssignedPatients { get; set; }
    }
}