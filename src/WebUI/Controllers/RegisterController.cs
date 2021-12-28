using Application.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IRegisterRepository _registerRepository;

        public RegisterController(IRegisterRepository registerRepository)
        {
            _registerRepository = registerRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<bool> TryRegister(string email, string password, string name, string phone, string license)
        {
            bool status;

            switch (license)
            {
                case null:
                    status = await _registerRepository.TryRegister(email, password, name, phone);
                    break;
                default:
                    status = await _registerRepository.TryRegisterInExistLicense(email, password, name, phone, license);
                    break;
            }

            return status;
        }
    }
}
