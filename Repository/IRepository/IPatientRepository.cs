using System.Threading.Tasks;
using HMS.Models;

namespace HMS.Repository.IRepository
{
    public interface IPatientRepository : IRepository<Patient>
    {
        bool UpdatePatient(Patient patient);
    }
}