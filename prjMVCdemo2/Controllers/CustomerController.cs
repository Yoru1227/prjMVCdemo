using prjWebDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjWebDemo.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return RedirectToAction("List");
            CCustomer x = new CCustomerFactory().queryById((int)id);
            if(x == null)
                return RedirectToAction("List");
            return View(x);
        }
        [HttpPost]
        public ActionResult Edit(CCustomer p)
        {           
            new CCustomerFactory().update(p);
            return RedirectToAction("List");
        }
        public ActionResult Delete(int? id)
        {
            if(id != null)
            {
                new CCustomerFactory().delete((int)id);
            }          
            return RedirectToAction("List");
        }
        public ActionResult List()
        {
            string keyword = Request.Form["txtKeyword"];
            List<CCustomer> data = null;
            if (string.IsNullOrEmpty(keyword))
                data = new CCustomerFactory().queryAll();
            else
                data = new CCustomerFactory().getByKeyword(keyword);
            return View(data);
        }
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Save()
        {
            CCustomer p = new CCustomer();
            p.fName = Request.Form["txtName"];
            p.fPhone = Request.Form["txtPhone"];
            p.fEmail = Request.Form["txtEmail"];
            p.fAddress = Request.Form["txtAddress"];
            p.fPassword = Request.Form["txtPassword"];
            new CCustomerFactory().create(p);
            return RedirectToAction("List");
        }
    }
}