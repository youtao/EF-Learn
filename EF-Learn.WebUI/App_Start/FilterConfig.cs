using System.Web.Mvc;
using EF_Learn.WebUI.Filter;

namespace EF_Learn.WebUI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filter)
        {
            filter.Add(new CustAuthorizeAttribute());
            filter.Add(new CustExceptionAttribute());
            filter.Add(new CustActionResultAttribute());
        }
    }
}