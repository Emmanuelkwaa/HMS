using System.Linq;
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

        public bool UpdateSchedule(Schedule schedule)
        {
            var existingSchedule = dbSet.Where(s => s.Id == schedule.Id).AsNoTracking();

            if (existingSchedule == null) return false;
            
            _db.Update(schedule);
            return true;
        }
    }
}