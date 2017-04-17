using System;
using Solar.Web.Utility;

namespace Application.Website
{
    public class CookieManager : CookieManagerBase
    {
        public CookieManager() : base()
        {
        }

        private static readonly CookieManager instance = new CookieManager();

        public static CookieManager Instance
        {
            get { return instance; }
        }

        [Expire("CookieTimeoutInDays")]
        public int? GuestId
        {
            get
            {
                int guestId;

                var cookie = GetCookieValue("GuestId");

                if (cookie == null || !Int32.TryParse(cookie.Value, out guestId))
                {
                    return null;
                }

                return guestId;
            }
            set
            {
                SetCookieValue("GuestId", value.ToString());
            }
        }        
    }
}