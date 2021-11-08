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

        public async Task<bool> UpdateNurse(Nurse patient)
        {
            var existingNurse = await _db.Nurses.FirstOrDefaultAsync(p => p.Id == patient.Id);

            if (existingNurse == null) return false;
            
            _db.Update(patient);
            return true;
        }
    }
}