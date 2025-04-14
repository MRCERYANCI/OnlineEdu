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

       

    }
}
