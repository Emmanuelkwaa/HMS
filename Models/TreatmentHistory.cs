using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HMS.Models
{
    public class TreatmentHistory
    {
        public int Id { get; set; }
        public Appointment Appointment { get; set; }
        
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public Patient Patient { get; set; }
        [Required]
        public string Disease { get; set; }
        [Required]
        public string Treatment { get; set; }

        public int DoctorId { get; set; }
        [ForeignKey("DoctorId")]
        public Doctor Doctor { get; set; }
    }
}