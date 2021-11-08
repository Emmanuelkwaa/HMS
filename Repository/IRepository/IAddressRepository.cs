using System.Threading.Tasks;
using HMS.Models;

namespace HMS.Repository.IRepository
{
    public interface IAddressRepository : IRepository<Address>
    {
        bool UpdateAddress(Address address);
    }
}