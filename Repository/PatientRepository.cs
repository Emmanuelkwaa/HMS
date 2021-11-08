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

        public async Task<bool> UpdatePatient(Patient patient)
        {
            var existingPatient = await _db.Patients.FirstOrDefaultAsync(p => p.Id == patient.Id);

            if (existingPatient == null) return false;
            
            _db.Update(patient);
            return true;
        }
    }
}