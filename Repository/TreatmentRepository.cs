using System.Linq;
using HMS.Models;
using HMS.Repository.IRepository;
using HSM.Repository;
using System.Threading.Tasks;
using HMS.Data;
using Microsoft.EntityFrameworkCore;

namespace HMS.Repository
{
    public class TreatmentHistoryRepository : Repository<TreatmentHistory>, ITreatmentHistoryRepository
    {
        private readonly ApplicationDbContext _db;
        public TreatmentHistoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public bool UpdateTreatmentHistory(TreatmentHistory treatmentHistory)
        {
            var existingSchedule = dbSet.Where(s => s.Id == treatmentHistory.Id).AsNoTracking();

            if (existingSchedule == null) return false;
            
            _db.Update(treatmentHistory);
            return true;
        }
    }
}