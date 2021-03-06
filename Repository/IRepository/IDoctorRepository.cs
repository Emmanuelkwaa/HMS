using System.Threading.Tasks;
using HMS.Models;

namespace HMS.Repository.IRepository
{
    public interface IDoctorRepository : IRepository<Doctor>
    {
        bool UpdateDoctor(Doctor patient);
    }
}