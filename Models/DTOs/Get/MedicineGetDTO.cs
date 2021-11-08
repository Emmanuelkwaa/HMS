using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HMS.Models.DTOs.Get
{
    public class MedicineGetDTO
    {
        public int Id { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public int QuantityGiven { get; set; }
        public int QuantityAvailable { get; set; }
        [Required]
        public double Price { get; set; }
        public int BillId { get; set; }
    }
}