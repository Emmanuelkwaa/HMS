using System.Threading.Tasks;
using HMS.Models;

namespace HMS.Repository.IRepository
{
    public interface IAppointmentRepository : IRepository<Appointment>
    {
        bool UpdateAppointment(Appointment appointment);
    }
}