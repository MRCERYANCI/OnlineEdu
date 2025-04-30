using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineEdu.DtoLayer.Dtos.RoleDtos;
using OnlineEdu.EntityLayer.Entities;
using System.Data;

namespace OnlineEdu.API.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController(RoleManager<AppRole> _roleManager,IMapper _mapper) : ControllerBase
    {
        [HttpGet("tum-rolleri-listele")]
        public async Task<IActionResult> GetAllRoles()
        {
            return Ok(_mapper.Map<List<ResultRoleDto>>(await _roleManager.Roles.ToListAsync()));
        }

        [HttpPost("rol-olustur")]
        public async Task<IActionResult> CreateRole(CreateRoleDto createRoleDto)
        {
            var role = _mapper.Map<AppRole>(createRoleDto);
            var result = await _roleManager.CreateAsync(role);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);   
            }
            return Ok(role);
        }

        [HttpDelete("rol-sil/{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var value = await _roleManager.Roles.FirstOrDefaultAsync(x => x.Id == id);
            if (value == null) 
            {
                return BadRequest("Role Bulunamadı");
            }

            var result = await _roleManager.DeleteAsync(value);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            return Ok("Silme Başarıyla Gerçekleşti");
        }
    }
}
