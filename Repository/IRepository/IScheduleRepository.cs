using System.Threading.Tasks;
using HMS.Models;

namespace HMS.Repository.IRepository
{
    public interface IScheduleRepository : IRepository<Schedule>
    {
        bool UpdateSchedule(Schedule schedule);
    }
}