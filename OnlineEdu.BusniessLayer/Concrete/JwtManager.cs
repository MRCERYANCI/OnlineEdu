using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OnlineEdu.BusinessLayer.Abstract;
using OnlineEdu.BusinessLayer.Configurations;
using OnlineEdu.DtoLayer.Dtos.JwtDtos;
using OnlineEdu.EntityLayer.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OnlineEdu.BusinessLayer.Concrete
{
    public class JwtManager : IJwtService
    {
        private readonly JwtTokenOptions _jwtOptions;
        private readonly UserManager<AppUser> _userManager;

        public JwtManager(IOptions<JwtTokenOptions> jwtOptions, UserManager<AppUser> userManager)
        {
            _jwtOptions = jwtOptions.Value;
            _userManager = userManager;
        }

        public async Task<LoginResponseDto> CreateTokenAsync(AppUser appUser)
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
            var userRoles = await _userManager.GetRolesAsync(appUser);
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,appUser.Id.ToString()),
                new Claim(ClaimTypes.Email,appUser.Email),
                new Claim(ClaimTypes.Name,appUser.UserName),
                new Claim(ClaimTypes.MobilePhone,appUser.PhoneNumber),
                new Claim("FullName",appUser.FirsName + " " + appUser.LastName),
            };

            foreach (var roles in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, roles));
            }

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(issuer: _jwtOptions.Issuer, audience: _jwtOptions.Audience, claims: claims, notBefore: DateTime.UtcNow, expires: DateTime.Now.AddMinutes(_jwtOptions.ExpireInMinutes), signingCredentials: new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256));

            var handler = new JwtSecurityTokenHandler();
            var responseDto = new LoginResponseDto();
            responseDto.Token = handler.WriteToken(jwtSecurityToken);
            responseDto.ExpireDate = DateTime.Now.AddMinutes(_jwtOptions.ExpireInMinutes);

            return responseDto;
        }
    }
}
