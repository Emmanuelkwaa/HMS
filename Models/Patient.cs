using System.ComponentModel.DataAnnotations;

namespace HMS.Models
{
    public class Patient
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
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
        public Doctor Doctor { get; set; }
        public Nurse Nurse { get; set; }
    }
}
