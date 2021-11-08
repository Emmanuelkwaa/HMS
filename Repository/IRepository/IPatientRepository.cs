using System.Threading.Tasks;
using HMS.Models;

namespace HMS.Repository.IRepository
{
    public interface IPatientRepository : IRepository<Patient>
    {
        Task<bool> UpdatePatient(Patient patient);
    }
}