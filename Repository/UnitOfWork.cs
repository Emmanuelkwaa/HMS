using HMS.Data;
using HMS.Repository.IRepository;
using System.Threading.Tasks;
using HMS.Models;

namespace HMS.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Patient = new PatientRepository(_db);
            Address = new AddressRepository(_db);
            Doctor = new DoctorRepository(_db);
            Bill = new BillRepository(_db);
        }
        
        public IPatientRepository Patient { get; set; }
        public IAddressRepository Address { get; set; }
        public IDoctorRepository Doctor { get ; set; }
        public INurseRepository Nurse { get ; set; }
        public IBillRepository Bill { get; set; }

        public void Dispose()
        {
            _db.Dispose();
        }

        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }
    }
}
