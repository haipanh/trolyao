using Solar.Security;

namespace Application.Website
{
    public static class VisitContext
    {
        public static CookieManager Cookie { get { return CookieManager.Instance; } }
        public static SessionManager Session { get { return SessionManager.Instance; } }

        public static IUserPrincipal Visitor
        {
            get
            {
                if (VisitContext.Session.Visitor == null)
                {
                    VisitContext.Session.Visitor = AuthenticationService.IdentifyVisitor();
                }

                return VisitContext.Session.Visitor;
            }
        }
    }
}