using System.Linq;
using HMS.Models;
using HMS.Repository.IRepository;
using HSM.Repository;
using System.Threading.Tasks;
using HMS.Data;
using Microsoft.EntityFrameworkCore;

namespace HMS.Repository
{
    public class PatientRepository : Repository<Patient>, IPatientRepository
    {
        private readonly ApplicationDbContext _db;
        public PatientRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public bool UpdatePatient(Patient patient)
        {
            var existingPatient = dbSet.Where(p => p.Id == patient.Id).AsNoTracking();
        
            if (existingPatient == null) return false;
            
            dbSet.Update(patient);
            return true;
        }
    }
}