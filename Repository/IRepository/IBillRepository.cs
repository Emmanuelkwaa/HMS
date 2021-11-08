using System.Threading.Tasks;
using HMS.Models;

namespace HMS.Repository.IRepository
{
    public interface IBillRepository : IRepository<Bill>
    {
        Task<bool> UpdateBill(Bill address);
    }
}