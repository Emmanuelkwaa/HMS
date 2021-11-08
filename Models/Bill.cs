using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HMS.Models
{
    public class Bill
    {
        [Key]
        public int Id { get; set; }
        public int MedicineId { get; set; }
        [ForeignKey("BillId")]
        public ICollection<Medicine> Medicine { get; set; }
        public double TotalCost { get; set; }
        public Patient Patient { get; set; }
    }
}