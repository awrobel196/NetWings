using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;


namespace Application.Common.Behaviours
{
    public class RegisterRepository : IRegisterRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILicenseRepository _licenseRepository;
        public RegisterRepository(ApplicationDbContext context, ILicenseRepository licenseRepository)
        {
            _context = context;
            _licenseRepository = licenseRepository;
        }
        public async Task<bool> TryRegister(string email, string password, string name, string phone)
        {
            if (_context.User.Any(x => x.Email == email))
            {
                return false;
            }
            License newUserLicense = await _licenseRepository.CreateLicense();

            User user = new User()
            {
                Email = email,
                Password = password,
                Name = name,
                Phone = phone,
                IdLicense = newUserLicense.IdLicense
            };

            _context.Add(user);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> TryRegisterInExistLicense(string email, string password, string name, string phone, string license)
        {
            if (!_context.License.Any(x => x.IdLicense.ToString().Contains(license)))
            {
                return false;
            }

            License existLicense = await _context.License.Where(x => x.IdLicense.ToString().Contains(license)).FirstAsync();

            if (_context.User.Any(x => x.Email == email))
            {
                return false;
            }

            if (existLicense == null)
            {
                return false;
            }

            User user = new User()
            {
                Email = email,
                Password = password,
                Name = name,
                Phone = phone,
                IdLicense = existLicense.IdLicense
            };

            _context.Add(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
