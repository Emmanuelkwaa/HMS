using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HMS.Models
{
    public class Medicine
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public int Quantity { get; set; }
        public int QuantityAvailable { get; set; }
        [Required]
        public double Price { get; set; }
        //public int BillId { get; set; }
        //[ForeignKey("BillId")] 
        //public Bill Bill { get; set; }
    }
}