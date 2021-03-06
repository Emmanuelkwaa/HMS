using AutoMapper;
using HMS.Models;
using HMS.Models.DTOs.Create;
using HMS.Models.DTOs.Get;

namespace HMS.Mapper
{
    public class HSMMappings : Profile
    {
        public HSMMappings()
        {
            CreateMap<Patient, PatientGetDTO>().ReverseMap();
            CreateMap<Patient, PatientCreateDTO>().ReverseMap();
            CreateMap<Patient, PatientUpdateDTO>().ReverseMap();
            CreateMap<Patient, PatientReceptionGetDTO>().ReverseMap();
            CreateMap<Address, AddressGetDTO>().ReverseMap();
            CreateMap<Address, AddressCreateDTO>().ReverseMap();
            CreateMap<Address, AddressUpdateDTO>().ReverseMap();
            CreateMap<Doctor, DoctorGetDTO>().ReverseMap();
            CreateMap<Doctor, DoctorReceptionGetDTO>().ReverseMap();
            CreateMap<Doctor, DoctorCreateDTO>().ReverseMap();
            CreateMap<Doctor, DoctorUpdateDTO>().ReverseMap();
            CreateMap<Doctor, PatientDoctorUpdateDTO>().ReverseMap();
            CreateMap<Nurse, NurseGetDTO>().ReverseMap();
            CreateMap<Bill, BillGetDTO>().ReverseMap();
            CreateMap<Bill, BillCreateDTO>().ReverseMap();
            CreateMap<Medicine, MedicineGetDTO>().ReverseMap();
            CreateMap<Appointment, AppointmentGetDTO>().ReverseMap();
            CreateMap<Appointment, AppointmentCreateDTO>().ReverseMap();
            CreateMap<Schedule, ScheduleGetDTO>().ReverseMap();
            CreateMap<Schedule, ScheduleCreateDTO>().ReverseMap();
        }
    }
}
