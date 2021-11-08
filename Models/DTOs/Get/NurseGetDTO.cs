namespace HMS.Models.DTOs.Get
{
    public class NurseGetDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Sex { get; set; }
        public AddressGetDTO Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}