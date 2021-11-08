using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HMS.Models
{
    public class Room
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string RoomNumber { get; set; }
        public DateTime Period { get; set; }
        [Required]
        public bool IsAvailable { get; set; }
    }
}