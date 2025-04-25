using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineEdu.DtoLayer.Dtos.UserDtos;
using OnlineEdu.EntityLayer.Entities;
using OnlineEdu.PresentationLayer.Services.MailServices;
using OnlineEdu.PresentationLayer.Services.UserService;
using System.Text;

namespace OnlineEdu.PresentationLayer.Controllers
{
    public class AccountController(IUserService _userService, UserManager<AppUser> _userManager, IEmailSender _emailSender) : Controller
    {
        private static readonly HttpClient client = new HttpClient();

        public IActionResult Login()
        {
            return View();
        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            var userRole = await _userService.LoginAsync(userLoginDto);

            var user = await _userManager.FindByEmailAsync(userLoginDto.Email);

            if (userRole == null)
                ModelState.AddModelError("", "Mail veya Şifre Hatalı");
            else if (userRole == "false")
            {
                await ResendEmailConfirmation(userLoginDto.Email);
                ModelState.AddModelError("", "Mail Adresiniz Onaylı Değildir Lütfen Mail Adresinize Gelen Linki Onaylayın");
            }
            else if (userRole == "Admin")
                return RedirectToAction("Index", "About", new { area = "Admin" });
            else if (userRole == "Teacher")
                return RedirectToAction("Index", "MyCourse", new { area = "Teacher" });
            else if (userRole == "Student")
                return RedirectToAction("Index", "CourseRegister", new { area = "Student" });

            return View();
        }

        public async Task<IActionResult> LogOut()
        {
            await _userService.LogoutAsync();
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
            if(user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
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
        public async Task<IActionResult> ResetPassword(string token,string email)
        {
            if(token == null || email == null)
                ModelState.AddModelError("", "Geçersiz şifre sıfırlama isteği.");

            var model = new ResetPasswordDto{ Token = token, Email = email };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            if(!ModelState.IsValid) { return View(); }

            var user = await _userManager.FindByEmailAsync(resetPasswordDto.Email);
            
            if(user == null)
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
