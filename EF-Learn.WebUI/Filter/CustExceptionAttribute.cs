using System.Web.Mvc;

namespace EF_Learn.WebUI.Filter
{
    /// <summary>
    /// 异常过滤器
    /// </summary>
    public class CustExceptionAttribute: FilterAttribute,IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            throw new System.NotImplementedException();
        }        
    }
}