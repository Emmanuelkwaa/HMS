using System;

namespace HMS.Models.DTOs.Create
{
    public class ScheduleCreateDTO
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}