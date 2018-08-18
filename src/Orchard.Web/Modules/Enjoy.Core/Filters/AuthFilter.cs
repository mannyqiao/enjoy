
namespace Enjoy.Core.Filters
{
    using Orchard;
    using Orchard.Mvc.Filters;
    using System.IO;
    using System.Web.Mvc;

    public class AuthFilter : FilterProvider, IResultFilter
    {
        private readonly IEnjoyAuthService _auth;
        private readonly WorkContext _workContext;
        public AuthFilter(IEnjoyAuthService auth, WorkContext workContext)
        {
            this._auth = auth;
            this._workContext = workContext;
        }
        public void OnResultExecuted(ResultExecutedContext filterContext)

        {

        }

        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
            if (this._auth.GetAuthenticatedUser() != null)
            {
                this._workContext.SetState<IEnjoyUser>("EnjoyUser", this._auth.GetAuthenticatedUser());
            }
            
            if (this._workContext.HttpContext.Request.RawUrl.EndsWith(".txt"))
            {
                var staticTxtFile = this._workContext.HttpContext.Server.MapPath(this._workContext.HttpContext.Request.RawUrl);
                if (File.Exists(staticTxtFile))
                {
                    this._workContext.HttpContext.Response.WriteFile(staticTxtFile);
                    this._workContext.HttpContext.Response.End();
                }
            }
        }
    }
}