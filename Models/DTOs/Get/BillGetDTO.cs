using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HMS.Models.DTOs.Get
{
    public class BillGetDTO
    {
        public int Id { get; set; }

        //public ICollection<MedicineGetDTO> Medicine { get; set; }
        public ICollection<Medicine> Medicine { get; set; }

        public double TotalCost { get; set; }
        
        public int PatientId { get; set; }
    }
}