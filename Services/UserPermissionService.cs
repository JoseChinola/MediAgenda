using AutoMapper;
using MediAgenda.DTOs.UserPermission;
using MediAgenda.Entities;
using MediAgenda.Interface.IUserPermission;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace MediAgenda.Services
{
    public class UserPermissionService : IUserPermissionService
    {
        private readonly IUserPermissionRepository _repository;
        private readonly IMapper _mapper;

        public UserPermissionService(IUserPermissionRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> AssignPermission(UserPermissionDto userPermissionDto)
        {
            var assigned = _mapper.Map<UserPermission>(userPermissionDto);

            var added = await _repository.AssignPermissionAsync(assigned);

            if (!added)
                return false;

            await _repository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemovePermission(UserPermissionDto userPermissionDto)
        {
            var remove = _mapper.Map<UserPermission>(userPermissionDto);
             _repository.RemovePermissionAsync(remove);          
            await _repository.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<UserPermissionResponseDto>> GetUserPermissions(Guid userId)
        {
            var userPermissions = await _repository.GetUserPermissionsAsync(userId);
            if (userPermissions == null || !userPermissions.Any())
            {
                return Enumerable.Empty<UserPermissionResponseDto>();
            }

            return _mapper.Map<IEnumerable<UserPermissionResponseDto>>(userPermissions);
        }

        public async Task<bool> HasPermission(Guid userId, string permissionName)
        {
            return await _repository.HasPermissionAsync(userId, permissionName);            
        }
    }
    
}
