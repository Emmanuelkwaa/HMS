using System.ComponentModel.DataAnnotations;

namespace HMS.Models
{
    public class Department
    {
        public int Id { get; set; }
        [Required]
        public string DepartmentName { get; set; }
        public string Description { get; set; }
    }
}