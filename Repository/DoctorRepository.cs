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

        public async Task<bool> UpdateDoctor(Doctor patient)
        {
            var existingDoctor = await _db.Doctors.FirstOrDefaultAsync(p => p.Id == patient.Id);

            if (existingDoctor == null) return false;
            
            _db.Update(patient);
            return true;
        }
    }
}