using System.Web;
using Solar.Security;
using Solar.Web.Utility;

namespace Application.Website
{
    public class SessionManager : SessionManagerBase
    {
        public SessionManager() : base()
        {
        }

        private static readonly SessionManager instance = new SessionManager();

        public static SessionManager Instance
        {
            get { return instance; }
        }

        public IUserPrincipal Visitor
        {
            get { return (IUserPrincipal)HttpContext.Current.Session["Visitor"]; }
            set { HttpContext.Current.Session["Visitor"] = value; }
        }
    }
}