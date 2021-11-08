using System.ComponentModel.DataAnnotations;

namespace HMS.Models.DTOs.Get
{
    public class AddressGetDTO
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string ApartmentNumber { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Zipcode { get; set; }
        public string AddressType { get; set; }
    }
}