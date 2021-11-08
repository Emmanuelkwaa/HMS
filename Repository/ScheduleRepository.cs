using HMS.Models;
using HMS.Repository.IRepository;
using HSM.Repository;
using System.Threading.Tasks;
using HMS.Data;
using Microsoft.EntityFrameworkCore;

namespace HMS.Repository
{
    public class ScheduleRepository : Repository<Schedule>, IScheduleRepository
    {
        private readonly ApplicationDbContext _db;
        public ScheduleRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<bool> UpdateSchedule(Schedule schedule)
        {
            var existingSchedule = await _db.Schedules.FirstOrDefaultAsync(s => s.Id == schedule.Id);

            if (existingSchedule == null) return false;
            
            _db.Update(schedule);
            return true;
        }
    }
}