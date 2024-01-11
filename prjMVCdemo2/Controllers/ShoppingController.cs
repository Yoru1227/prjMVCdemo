using prjMVCdemo2.Models;
using prjMVCdemo2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjMVCdemo2.Controllers
{
    public class ShoppingController : Controller
    {
        // GET: Shopping
        public ActionResult List()
        {
            string keyword = Request.Form["txtKeyword"];
            dbDemoEntities db = new dbDemoEntities();
            var data = from p in db.tProduct select p;            
            return View(data);
        }
        public ActionResult AddToCart(int? id)
        {
            if (id == null) 
                return RedirectToAction("List");
            ViewBag.FID = id;
            return View();
        }
        [HttpPost]
        public ActionResult AddToCart(CAddToCartViewModel vm)
        {
            dbDemoEntities db = new dbDemoEntities();
            tProduct product = db.tProduct.FirstOrDefault(p => p.fId == vm.txtFId);
            if(product != null)
            {
                tShoppingCart item = new tShoppingCart();
                item.fDate = DateTime.Now.ToString("yyyyMMddHHmmss");
                item.fCustomerId = 1;
                item.fProductId = vm.txtFId;
                item.fPrice = product.fPrice;
                item.fCount = vm.txtCount;
                db.tShoppingCart.Add(item);
                db.SaveChanges();
            }
            return RedirectToAction("List");
        }
        public ActionResult CartView()
        {
            List<CShoppingCartItem> cart = Session[CDictionary.SK_UNPURCHASED_PRODUCT_LIST] as List<CShoppingCartItem>;
            if (cart == null)
                return RedirectToAction("List");
            return View(cart);
        }
        public ActionResult AddToSession(int? id)
        {
            if (id == null)
                return RedirectToAction("List");
            ViewBag.FID = id;
            return View();
        }
        [HttpPost]
        public ActionResult AddToSession(CAddToCartViewModel vm)
        {
            dbDemoEntities db = new dbDemoEntities();
            tProduct product = db.tProduct.FirstOrDefault(p => p.fId == vm.txtFId);
            if (product != null)
            {
                List<CShoppingCartItem> cart = Session[CDictionary.SK_UNPURCHASED_PRODUCT_LIST] as List<CShoppingCartItem>;
                if(cart == null)
                {
                    cart = new List<CShoppingCartItem>();
                    Session[CDictionary.SK_UNPURCHASED_PRODUCT_LIST] = cart;
                }
                CShoppingCartItem x = new CShoppingCartItem();
                x.price = (decimal)product.fPrice;
                x.count = vm.txtCount;
                x.productId = vm.txtFId;
                x.product = product;
                cart.Add(x);
            }
            return RedirectToAction("List");
        }
        public ActionResult Index()
        {
            return View();
        }
    }
}