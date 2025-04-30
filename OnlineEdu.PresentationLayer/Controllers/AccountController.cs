using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OnlineEdu.DtoLayer.Dtos.JwtDtos;
using OnlineEdu.DtoLayer.Dtos.UserDtos;
using OnlineEdu.EntityLayer.Entities;
using OnlineEdu.PresentationLayer.Helpers;
using OnlineEdu.PresentationLayer.Models;
using OnlineEdu.PresentationLayer.Services.MailServices;
using OnlineEdu.PresentationLayer.Services.TokenServices;
using OnlineEdu.PresentationLayer.Services.UserService;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace OnlineEdu.PresentationLayer.Controllers
{
    public class AccountController(IUserService _userService, UserManager<AppUser> _userManager, IEmailSender _emailSender,ITokenService _tokenService) : Controller
    {
        private readonly HttpClient _httpClient = HttpClientInstance.CreateClient();

        public IActionResult Login()
        {
            return View();
        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {

            var result = await _httpClient.PostAsJsonAsync("Accounts", userLoginDto);
            if (!result.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Şuanda İşleminizi Gerçekleştiremiyoruz Lütfen Daha Sonra Tekrar Deneyiniz");
                return View();
            }

            var content = await result.Content.ReadAsStringAsync();
            if (string.IsNullOrWhiteSpace(content))
            {
                ModelState.AddModelError("", "Sunucudan boş yanıt alındı.");
                return View();
            }

            var response = JsonSerializer.Deserialize<UserResponseJwtDto>(content);

            if (response.statusCode == 404)
            {
                ModelState.AddModelError("", response.message);
                return View();
            }

            if (response.statusCode == 202)
            {
                await ResendEmailConfirmation(userLoginDto.Email);
                ModelState.AddModelError("", response.message);
                return View();
            }

            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(response.token);
            var claims = token.Claims.ToList();

            if (response.token != null)
            {
                if (!string.IsNullOrEmpty(response.token))
                {
                    claims.Add(new Claim("Token", response.token));
                }
                var claimsIdentity = new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme);
                var authProps = new AuthenticationProperties()
                {
                    ExpiresUtc = response.expireDate,
                    IsPersistent = userLoginDto.RememberMe
                };

                await HttpContext.SignInAsync(JwtBearerDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProps);

                // Role bul
                var role = claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                // Role göre yönlendir
                if (role == "Admin")
                {
                    return RedirectToAction("Index", "About", new { area = "Admin" });
                }
                else if (role == "Teacher")
                {
                    return RedirectToAction("Index", "MyCourse", new { area = "Teacher" });
                }
            }

            ModelState.AddModelError("", "Role Bilgisi Bulunamadı Lütfen Sistem Yöneticinizle İletişime Geçiniz");
            return View();
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }

        public async Task ResendEmailConfirmation(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = Url.Action("ConfirmEmail", "Account",
                new { userId = user.Id, token = token }, Request.Scheme);

            await _emailSender.SendConfirmedEmailAsync(user.Email, user.FirsName + " " + user.LastName, user.UserName, confirmationLink);
        }

        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return View();
            }

            return BadRequest();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpGet("sifremi-unuttum")]
        public async Task<IActionResult> ForgotPassword()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost("sifremi-unuttum")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto forgotPasswordDto)
        {
            if (!ModelState.IsValid) { return View(); }

            var user = await _userManager.FindByEmailAsync(forgotPasswordDto.Email);
            if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
            {
                var emailToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var confirmationLink = Url.Action("ConfirmEmail", "Account",
                    new { userId = user.Id, token = emailToken }, Request.Scheme);

                await _emailSender.SendConfirmedEmailAsync(user.Email, user.FirsName + " " + user.LastName, user.UserName, confirmationLink);
                ModelState.AddModelError("", "Mail Adresiniz Onaylı Olamadığından Şifre Sıfırlama Talebi Gönderilmedi Önce " + forgotPasswordDto.Email + " Mail Adresinize Gelen Kodu Doğrulayınız");
                return View();
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var resetLink = Url.Action("ResetPassword", "Account",
                    new { token = token, email = user.Email }, Request.Scheme);

            await _emailSender.SendForgotPassword(forgotPasswordDto.Email, resetLink, user.UserName, user.FirsName + " " + user.LastName);

            ModelState.AddModelError("", "Şifre Sıfırlama Talebi " + forgotPasswordDto.Email + " Mail Adresinize Gönderilmiştir");

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ResetPassword(string token, string email)
        {
            if (token == null || email == null)
                ModelState.AddModelError("", "Geçersiz şifre sıfırlama isteği.");

            var model = new ResetPasswordDto { Token = token, Email = email };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            if (!ModelState.IsValid) { return View(); }

            var user = await _userManager.FindByEmailAsync(resetPasswordDto.Email);

            if (user == null)
                ModelState.AddModelError("", "Geçersiz şifre sıfırlama isteği.");

            var result = await _userManager.ResetPasswordAsync(user, resetPasswordDto.Token, resetPasswordDto.Password);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Login));
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View();
        }
    }
}
