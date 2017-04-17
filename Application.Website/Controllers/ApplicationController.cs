using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Solar.Web.Mvc;
using System.Net;

namespace Application.Website.Controllers
{
    public class ApplicationController : Controller
    {
        protected override void HandleUnknownAction(string actionName)
        {
            this.RedirectToAction(c => c.UnresolvableRequest(this.Request.Url.AbsoluteUri))
                .ExecuteResult(this.ControllerContext);
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled)
            {
                filterContext.ExceptionHandled = true;

                var signaledError = filterContext.Exception is HttpException
                    ? filterContext.Exception
                    : new HttpException((int)HttpStatusCode.InternalServerError, filterContext.Exception.Message, filterContext.Exception);

                Elmah.ErrorSignal.FromCurrentContext().Raise(signaledError);

                //if (!filterContext.HttpContext.IsCustomErrorEnabled)
                //{
                    this.TempData["Description"] = filterContext.Exception.Message;
                    this.TempData["Source"] = filterContext.Exception.Source;
                    this.TempData["StackTrace"] = filterContext.Exception.StackTrace;
                //}

                this.RedirectToAction(c => c.Error()).ExecuteResult(this.ControllerContext);
            }
        }

        public ActionResult Error()
        {
            this.Response.TrySkipIisCustomErrors = true;
            this.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            this.ViewData["Description"] = this.TempData["Description"];
            this.ViewData["Source"] = this.TempData["Source"];
            this.ViewData["StackTrace"] = this.TempData["StackTrace"];

            return View();
        }

        public ViewResult UnresolvableRequest(string path)
        {
            this.Response.TrySkipIisCustomErrors = true;
            this.Response.StatusCode = (int)HttpStatusCode.NotFound;

            ViewData["path"] = string.IsNullOrEmpty(path) ? this.Request.Url.AbsoluteUri : path;

            return this.View();
        }
    }
}
