using System;
using HMS.Enums;

namespace HMS.Models
{
    public class Schedule
    {
        public int Id { get; set; }
        public Doctor Doctor { get; set; }
        public Day ScheduleDay { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public TimeSpan ShiftLength { get; set; }
    }
}