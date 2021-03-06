using System.ComponentModel.DataAnnotations;

namespace HMS.Models
{
    public class Address
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Street { get; set; }
        public string ApartmentNumber { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string Zipcode { get; set; }

        public string AddressType { get; set; } //Employee or Patient
    }
}