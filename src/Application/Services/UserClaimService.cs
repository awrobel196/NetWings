using System.Security.Claims;

namespace Application.Services
{
    public static class UserClaimService
    {
        public static Guid GetUserId(ClaimsPrincipal user)
        {
            var value = user.Identities.FirstOrDefault()?.Claims
                .Where(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")
                .Select(x => x.Value).FirstOrDefault();

            return new Guid(value);
        }

        public static string GetName(ClaimsPrincipal user)
        {
            var value = user.Identities.FirstOrDefault()?.Claims
                .Where(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")
                .Select(x => x.Value).FirstOrDefault();

            return value;
        }

        public static string GetEmail(ClaimsPrincipal user)
        {
            var value = user.Identities.FirstOrDefault()?.Claims
                .Where(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")
                .Select(x => x.Value).FirstOrDefault();

            return value;
        }

        public static Guid GetLicenseId(ClaimsPrincipal user)
        {
            var value = user.Identities.FirstOrDefault()?.Claims
                .Where(x => x.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/serialnumber")
                .Select(x => x.Value).FirstOrDefault();

            return new Guid(value);
        }
    }
}

