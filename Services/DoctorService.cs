using AutoMapper;
using MediAgenda.DTOs.Doctor;
using MediAgenda.Entities;
using MediAgenda.Interface.IDoctor;
using MediAgenda.Interface.IUser;

namespace MediAgenda.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IUserRepository _userRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IMapper _mapper;


        public DoctorService(IDoctorRepository doctorRepository, IMapper mapper, IUserRepository userRepository)
        {
            _doctorRepository = doctorRepository;
            _mapper = mapper;
            _userRepository = userRepository;
        }


        public async Task<DoctorDto> CreateAsync(CreateDoctorDto createDoctorDto)
        {
            var user = await _userRepository.GetByIdAsync(createDoctorDto.UserId);
            if(user == null)
                throw new Exception("Usuario no encontrado");

            if (user.Role?.Name != "Doctor")
                throw new Exception("El usuario no tiene el rol de Doctor");

            var doctor = _mapper.Map<Doctor>(createDoctorDto);
            await _doctorRepository.AddAsync(doctor);
            await _doctorRepository.SaveChangesAsync();


            return _mapper.Map<DoctorDto>(doctor);
        }

        public async Task<bool> DeleteAsync(Guid userId)
        {
            var doctor = await _doctorRepository.GetByIdAsync(userId);
            if (doctor == null) return false;
            
            _doctorRepository.Delete(doctor);
            return await _doctorRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<DoctorDto>> GetAllAsync()
        {
            var doctors = await _doctorRepository.GetAllAsync();
          
            return doctors.Select(d =>
            {
                var dto = _mapper.Map<DoctorDto>(d);
                dto.FullName = $"{d.User.FirstName} {d.User.LastName}";               
                return dto;
            });
        }

        public async Task<DoctorDto?> GetByIdAsync(Guid userId)
        {
            var doctor = await _doctorRepository.GetByIdAsync(userId);
            if(doctor == null) return null;

            var dto = _mapper.Map<DoctorDto?>(doctor);
            dto.FullName = $"{doctor.User.FirstName} {doctor.User.LastName}";       
            return dto;

        }

        public async Task<bool> UpdateAsync(Guid userId, CreateDoctorDto updateDoctorDto)
        {
            var doctor = await _doctorRepository.GetByIdAsync(userId);
            if (doctor == null) return false;

            _mapper.Map(updateDoctorDto, doctor);
            _doctorRepository.Update(doctor);

            return await _doctorRepository.SaveChangesAsync();
        }
    }
}
