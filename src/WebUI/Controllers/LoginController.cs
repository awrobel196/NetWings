using Application.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginRepository _loginRepository;

        public LoginController(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Logout()
        {
            _loginRepository.Logout();
            return RedirectToAction("Index","Login",new { type = "logout" });
        }

        [HttpPost]
        public async Task<bool> TryLogin(string email, string password)
        {
            var status = await _loginRepository.TryLogin(email, password);
            return status;
        }
    }
}
