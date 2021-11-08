using HMS.Models;
using HMS.Repository.IRepository;
using HSM.Repository;
using System.Threading.Tasks;
using HMS.Data;
using Microsoft.EntityFrameworkCore;

namespace HMS.Repository
{
    public class AppointmentRepository : Repository<Appointment>, IAppointmentRepository
    {
        private readonly ApplicationDbContext _db;
        public AppointmentRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<bool> UpdateAppointment(Appointment appointment)
        {
            var existingAppointment = await _db.Appointments.FirstOrDefaultAsync(a => a.Id == appointment.Id);

            if (existingAppointment == null) return false;
            
            _db.Update(appointment);
            return true;
        }
    }
}