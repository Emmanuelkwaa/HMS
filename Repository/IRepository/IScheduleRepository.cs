using System.Threading.Tasks;
using HMS.Models;

namespace HMS.Repository.IRepository
{
    public interface IScheduleRepository : IRepository<Schedule>
    {
        Task<bool> UpdateSchedule(Schedule schedule);
    }
}