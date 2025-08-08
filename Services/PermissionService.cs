using AutoMapper;
using MediAgenda.DTOs.Permission;
using MediAgenda.Entities;
using MediAgenda.Interface.IPermission;

namespace MediAgenda.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IPermissionRepository _repository;
        private readonly IMapper _mapper;

        public PermissionService(IPermissionRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PermissionDto> CreateAsync(CreatePermissionDto dto)
        {
            var normalizedName = dto.Name.Trim().ToLower();

            var exists = await _repository.ExistsByNameAsync(normalizedName);
            if (exists)
                return null;

            var permission = _mapper.Map<Permission>(dto);
            permission.Id = Guid.NewGuid();

            await _repository.AddAsync(permission);
            await _repository.SaveChangesAsync();

            return _mapper.Map<PermissionDto>(permission);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var permission = await _repository.GetByIdAsync(id);

            if (permission == null) return false;

            _repository.Delete(permission);
            return await _repository.SaveChangesAsync();
        }

        public async Task<IEnumerable<PermissionDto>> GetAllAsync()
        {
            var permissions = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<PermissionDto>>(permissions);
        }

        public async Task<PermissionDto?> GetByIdAsync(Guid id)
        {
            var permission = await _repository.GetByIdAsync(id);
            if (permission == null) return null;

            return _mapper.Map<PermissionDto>(permission);
        }

        public async Task<PermissionDto?> UpdateAsync(Guid id, CreatePermissionDto dto)
        {
            var permission = await _repository.GetByIdAsync(id);
            if (permission == null) return null;

            permission.Name = dto.Name;
            _repository.SaveChangesAsync();

            return _mapper.Map<PermissionDto>(permission);
        }
    }
}
