using System.Threading.Tasks;
using HMS.Models;

namespace HMS.Repository.IRepository
{
    public interface INurseRepository : IRepository<Nurse>
    {
        Task<bool> UpdateNurse(Nurse patient);
    }
}