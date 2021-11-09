using System.Threading.Tasks;
using HMS.Models;

namespace HMS.Repository.IRepository
{
    public interface ITreatmentHistoryRepository : IRepository<TreatmentHistory>
    {
        bool UpdateTreatmentHistory(TreatmentHistory treatmentHistory);
    }
}