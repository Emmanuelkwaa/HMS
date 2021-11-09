using System.Linq;
using HMS.Models;
using HMS.Repository.IRepository;
using HSM.Repository;
using System.Threading.Tasks;
using HMS.Data;
using Microsoft.EntityFrameworkCore;

namespace HMS.Repository
{
    public class NurseRepository : Repository<Nurse>, INurseRepository
    {
        private readonly ApplicationDbContext _db;
        public NurseRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public bool UpdateNurse(Nurse patient)
        {
            var existingNurse = dbSet.Where(p => p.Id == patient.Id).AsNoTracking();

            if (existingNurse == null) return false;
            
            _db.Update(patient);
            return true;
        }
    }
}