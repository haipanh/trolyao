using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Application.Website
{
    public static class StateManager
    {
        public static void InitializeHttpSession()
        {
            AuthenticationService.IdentifyVisitor();
        }
    }
}