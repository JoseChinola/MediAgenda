using AutoMapper;
using MediAgenda.DTOs.Reception;
using MediAgenda.Interface.IUser;
using MediAgenda.Interface.Reception;

namespace MediAgenda.Services
{
    public class ReceptionService : IReceptionService
    { 
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public ReceptionService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReceptionDto>> GetAllAsync()
        {
            var users = await _userRepository.GetAllWithRolesAsync();
            var receptions = users
                .Where(u => u.Role != null && u.Role.Name == "Reception")
                .Select(u => _mapper.Map<ReceptionDto>(u));

            return receptions;
        }

        public async Task<ReceptionDto?> GetByIdAsync(Guid id)
        {
            var user = await _userRepository.GetByIdWithRoleAsync(id);

            if (user == null || user.Role?.Name != "Reception") return null;
            
            return _mapper.Map<ReceptionDto?>(user);
        }
    }
}
