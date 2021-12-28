using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Application.Common.Interfaces;
using Domain.Common.ExtensionMethods;
using Domain.Entities;
using Infrastructure.Persistance;
using System.Web.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace Application.Common.Behaviours
{
    public class LoginRepository : Controller, ILoginRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext _context;

        public LoginRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> TryLogin(string email, string password)
        {
            var user = await _context.User.FirstOrDefaultAsync(x => x.Email == email);
            if (user != null && user.Password == password.ToHash()) return await Login(user); else return false;
        }

        private async Task<bool> Login(User user)
        {
            var claims = new List<Claim>() {
                new Claim(ClaimTypes.NameIdentifier, user.IdUser.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.SerialNumber, user.IdLicense.ToString())
            };
            //Initialize a new instance of the ClaimsIdentity with the claims and authentication scheme    
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            //Initialize a new instance of the ClaimsPrincipal with ClaimsIdentity    
            var principal = new ClaimsPrincipal(identity);
            //SignInAsync is a Extension method for Sign in a principal for the specified scheme.    
            //SignInAsync is a Extension method for Sign in a principal for the specified scheme.
            Thread.CurrentPrincipal = principal;
            
            await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties());
            return true;
        }

        public async Task Logout()
        {
            await _httpContextAccessor.HttpContext.SignOutAsync();
        }
    }
}
