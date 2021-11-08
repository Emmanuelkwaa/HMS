using System.Collections.Generic;

namespace HMS.Models.DTOs.Create
{
    public class BillCreateDTO
    {
        public int Id { get; set; }
        public ICollection<Medicine> Medicine { get; set; }
        public double TotalCost { get; set; }
        
        public int PatientId { get; set; }
    }
}