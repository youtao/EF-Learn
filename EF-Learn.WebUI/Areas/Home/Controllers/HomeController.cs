using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EF_Learn.IBLL;

namespace EF_Learn.WebUI.Areas.Home.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(IArticleBll articleBll)
        {
            ArticleBll = articleBll;
        }

        public IArticleBll ArticleBll { get; set; }
        public ActionResult Index()
        {
            ViewData.Model = this.ArticleBll.AllNotDelete().ToList();
            return View();
        }
    }
}