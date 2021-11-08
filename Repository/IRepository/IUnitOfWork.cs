using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HMS.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IPatientRepository Patient { get; set; }
        IAddressRepository Address { get; set; }
        IDoctorRepository Doctor { get; set; }
        INurseRepository Nurse { get; set; }
        IBillRepository Bill { get; set; }
        IAppointmentRepository Appointment { get; set; }
        IScheduleRepository Schedule { get; set; }

        Task Save();
    }
}
