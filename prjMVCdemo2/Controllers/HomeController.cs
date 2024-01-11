using prjMVCdemo2.ViewModels;
using prjWebDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjMVCdemo2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(CLoginViewModel vm)
        {
            CCustomer c = new CCustomerFactory().getByEmail(vm.txtAccount);
            if(c != null )
            {
                if(c.fPassword.Equals(vm.txtPassword))
                {
                    return RedirectToAction("Index");
                }
            }
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}