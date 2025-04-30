using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineEdu.BusinessLayer.Abstract;
using OnlineEdu.DtoLayer.Dtos.JwtDtos;
using OnlineEdu.DtoLayer.Dtos.UserDtos;
using OnlineEdu.EntityLayer.Entities;

namespace OnlineEdu.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController(UserManager<AppUser> _userManager, SignInManager<AppUser> _signInManager, IJwtService _jwtService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            var user = await _userManager.FindByEmailAsync(userLoginDto.Email);
            if (user == null)
                return Ok(new UserResponseDto{ StatusCode = 404, Message = "Kullanıcı adı ve/veya Şifre Yanlış" });

            if(!user.EmailConfirmed)
                return Ok(new UserResponseDto { StatusCode = 202, Message = "Mail Adresiniz Onaylı Değildir Lütfen Onaylayınız" });

            var result = await _signInManager.PasswordSignInAsync(user, userLoginDto.Password, userLoginDto.RememberMe, true);
            if (!result.Succeeded)
                return Ok(new UserResponseDto { StatusCode = 404, Message = "Kullanıcı adı ve/veya Şifre Yanlış" });

            var token = await _jwtService.CreateTokenAsync(user);

            return Ok(new UserResponseDto { StatusCode = 200, Token = token.Token, ExpireDate = token.ExpireDate });
        }
    }
}
