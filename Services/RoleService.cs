using AutoMapper;
using MediAgenda.DTOs.Roles;
using MediAgenda.Entities;
using MediAgenda.Interface.IRole;

namespace MediAgenda.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public RoleService(IRoleRepository roleRepository, IMapper mapper) 
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RoleDto>> GetAllAsync()
        {
            var roles =  await _roleRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<RoleDto>>(roles);
        }

        public async Task<RoleDto> GetByIdAsync(Guid id)
        {
            var role = await _roleRepository.GetByIdAsync(id);
            return _mapper.Map<RoleDto>(role);
        }

        public async Task<RoleDto> CreateAsync(CreateRoleDto createRoleDto)
        {
            var role = _mapper.Map<Role>(createRoleDto);
            await _roleRepository.AddAsync(role);
            await _roleRepository.SaveChangesAsync();
            return _mapper.Map<RoleDto>(role);
        }

        public async Task<RoleDto> UpdateAsync(Guid id, CreateRoleDto updateRoleDto)
        {
            var role = await _roleRepository.GetByIdAsync(id);
            if (role == null) return null;
            

            _mapper.Map(updateRoleDto, role);
            _roleRepository.Update(role);
            await _roleRepository.SaveChangesAsync();

            return _mapper.Map<RoleDto>(role);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var role = await _roleRepository.GetByIdAsync(id);
            if (role == null) return false;

            _roleRepository.Delete(role);
            return await _roleRepository.SaveChangesAsync();
        }

    }
}
