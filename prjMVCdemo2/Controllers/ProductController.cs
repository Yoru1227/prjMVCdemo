using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjMVCdemo2.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult List() 
        {
            string keyword = Request.Form["txtKeyword"];
            dbDemoEntities db = new dbDemoEntities();
            IEnumerable<tProduct> data = null;
            if (string.IsNullOrEmpty(keyword))
                data = from p in db.tProduct select p;
            else
                data = db.tProduct.Where(p => p.fName.Contains(keyword));

            return View(data);
        }
        public ActionResult Edit(int? id)
        {
            dbDemoEntities db = new dbDemoEntities();
            tProduct product = db.tProduct.FirstOrDefault(p => p.fId == id);
            if(product == null)
                return RedirectToAction("List");
            return View(product);
        }
        [HttpPost]
        public ActionResult Edit(tProduct pIn)
        {
            dbDemoEntities db = new dbDemoEntities();
            tProduct product = db.tProduct.FirstOrDefault(p => p.fId == pIn.fId);
            if (product != null)
            {
                if(pIn.photo != null)
                {
                    string photoName = Guid.NewGuid().ToString() + ".jpg";
                    product.fImagePath = photoName;
                    pIn.photo.SaveAs(Server.MapPath("..\\..\\images") + "\\" + photoName);
                }
                product.fName = pIn.fName;
                product.fQty = pIn.fQty;
                product.fCost = pIn.fCost;
                product.fPrice = pIn.fPrice;
                db.SaveChanges();
            }
            return RedirectToAction("List");          
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(tProduct p)
        {
            dbDemoEntities db = new dbDemoEntities();
            db.tProduct.Add(p);
            db.SaveChanges();
            return RedirectToAction("List");
        }
        public ActionResult Delete(int? id)
        {
            dbDemoEntities db = new dbDemoEntities();
            tProduct product = db.tProduct.FirstOrDefault(p => p.fId == id);
            if(product != null)
            {
                db.tProduct.Remove(product);
                db.SaveChanges();
            }
            return RedirectToAction("List");
        }
        public ActionResult Index()
        {
            return View();
        }
    }
}