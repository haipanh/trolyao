using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Solar.Security.Web;
using Solar.Security;
using System.Web.Security;
using Solar.Web.Utility;
using System.Configuration;
using Application.Service;

namespace Application.Website
{
    public class AuthenticationService : FormsAuthenticationServiceBase
    {
        public new const string AuthenticationType = "Website Authentication";
        public const string StaffAuthCookieName = "StaffAuthCookie";

        public AuthenticationService()
        {
            // TODO: apply configuration settings
            this.ExpirationTimeWindow = TimeSpan.FromDays(Double.Parse(ConfigurationManager.AppSettings["CookieTimeoutInDays"]));
            this.PersistsAuthentication = false;
            this.PersistsGuestSession = true;
        }

        protected bool PersistsGuestSession { get; set; }

        protected override IUserPrincipal GetUserCredential(string username, string password)
        {
            var service = new AdminService();

            return service.Authenticate(username, password);
        }

        protected override void OnAuthenticated(IUserPrincipal user)
        {
            VisitContext.Session.Abandon(); // new life that is legal

            base.OnAuthenticated(user);
        }

        protected override void OnSignedOut(IUserPrincipal user)
        {
            VisitContext.Session.Abandon(); // new life that is wild

            base.OnSignedOut(user);
        }

        public static IUserPrincipal IdentifyVisitor()
        {
            var visitor = HttpContext.Current.User as IUserPrincipal;

            //if (visitor == null || visitor == UserPrincipal.Unidentified || !visitor.UserIdentity.IsAuthenticated)
            //{
            //    visitor = CreateGuestPrincipal();
            //}

            return visitor;
        }
        
        private static IUserPrincipal CreateGuestPrincipal()
        {
            IUserPrincipal guest = null;

            // first, try client-side provided guest Id
            if (VisitContext.Cookie.GuestId.HasValue) // guest Id exists and is provided by client-side (browser)?
            {
                int guestId = VisitContext.Cookie.GuestId.Value;

                guest = CreateGuestPrincipal(guestId);
            }

            // if client does not provide guest Id or the provided guest Id is not valid, create one
            if (guest == null)
            {
                int guestId = 0;

                // create guest
                guest = CreateGuestPrincipal(guestId);
            }

            // persists client-side guest Id. If one has already existed, its timeout is reset
            VisitContext.Cookie.GuestId = guest.UserIdentity.Id;

            return guest;
        }

        private static IUserPrincipal CreateGuestPrincipal(int guestId)
        {
            IUserIdentity guestIdentity = new UserIdentity(guestId, String.Empty, String.Empty, String.Empty, false);

            IUserPrincipal guestPrincipal = new UserPrincipal(guestIdentity, new string[0]);

            return guestPrincipal;
        }
    }
}