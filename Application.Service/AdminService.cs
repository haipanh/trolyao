using Application.Domain;
using Solar.Core;
using Solar.Security;

namespace Application.Service
{
    public class AdminService
    {
        public IUserPrincipal Authenticate(string username, string password)
        {
            using (var repository = DomainRepository.Open())
            {
                var account = repository.GetOne<Admin>(c => c.Username == username);

                if (account != null && account.Password.Equals(SecurityService.GetMD5Hash(password)))
                {
                    return new UserPrincipal(new UserIdentity(account.Id, account.Username, account.Username, "admin", true), null);
                }

                return UserPrincipal.Unidentified;
            }
        }
    }
}
