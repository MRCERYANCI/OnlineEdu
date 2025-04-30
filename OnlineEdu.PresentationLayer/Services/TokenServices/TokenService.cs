using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace OnlineEdu.PresentationLayer.Services.TokenServices
{
    public class TokenService(IHttpContextAccessor _httpContextAccessor) : ITokenService
    {
        public string GetUserToken => _httpContextAccessor.HttpContext?.User?.Claims.FirstOrDefault(x => x.Type == "Token")?.Value;

        public int GetUserId => int.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

        public string GetUserRole => _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Role).Value;

        public string GetUserNameSurname => _httpContextAccessor.HttpContext.User.FindFirst("FullName").Value;

        public string GetUserName => _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value;
    }
}
