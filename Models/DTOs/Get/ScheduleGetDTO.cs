using System;
using HMS.Enums;
using HMS.Models.DTOs.Get;

namespace HMS.Models
{
    public class ScheduleGetDTO
    {
        public int Id { get; set; }
        public DoctorReceptionGetDTO Doctor { get; set; }
        public Day ScheduleDay { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public TimeSpan ShiftLength { get; set; }
    }
}