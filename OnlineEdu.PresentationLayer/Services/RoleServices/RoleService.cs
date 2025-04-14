using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using OnlineEdu.DtoLayer.Dtos.RoleDtos;
using OnlineEdu.EntityLayer.Entities;

namespace OnlineEdu.PresentationLayer.Services.RoleServices
{
    public class RoleService(RoleManager<AppRole> _roleManager, IMapper _mapper) : IRoleService
    {
        public async Task CreateRoleAsync(CreateRoleDto createRoleDto)
        {
            var role = _mapper.Map<AppRole>(createRoleDto);
            await _roleManager.CreateAsync(role);
        }

        public async Task DeleteRoleAsync(int id)
        {
            var value = await _roleManager.Roles.FirstOrDefaultAsync(x => x.Id == id);
            await _roleManager.DeleteAsync(value);
        }

        public async Task<List<ResultRoleDto>> GetAllRoleAsync()
        {
            return _mapper.Map<List<ResultRoleDto>>(await _roleManager.Roles.ToListAsync());
        }

        public async Task<UpdateRoleDto> GetRoleByIdAsync(int id)
        {
            return _mapper.Map<UpdateRoleDto>(await _roleManager.Roles.FirstOrDefaultAsync(x => x.Id == id));
        }
    }
}
