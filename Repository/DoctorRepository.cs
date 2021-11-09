using System.Linq;
using HMS.Models;
using HMS.Repository.IRepository;
using HSM.Repository;
using System.Threading.Tasks;
using HMS.Data;
using Microsoft.EntityFrameworkCore;

namespace HMS.Repository
{
    public class DoctorRepository : Repository<Doctor>, IDoctorRepository
    {
        private readonly ApplicationDbContext _db;
        public DoctorRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public bool UpdateDoctor(Doctor patient)
        {
            var existingDoctor = dbSet.Where(p => p.Id == patient.Id).AsNoTracking();

            if (existingDoctor == null) return false;
            
            _db.Update(patient);
            return true;
        }
    }
}