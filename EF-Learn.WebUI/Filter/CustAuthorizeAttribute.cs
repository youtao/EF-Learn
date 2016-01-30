using System.Web;
using System.Web.Mvc;

namespace EF_Learn.WebUI.Filter
{
    /// <summary>
    /// 权限过滤器
    /// </summary>
    public class CustAuthorizeAttribute:AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return true; // base.AuthorizeCore(httpContext);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            base.HandleUnauthorizedRequest(filterContext);
        }
    }
}