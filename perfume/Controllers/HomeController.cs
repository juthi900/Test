using perfume.fume;
using perfume.Models.Homemenu;
using perfume.reposit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace perfume.Controllers
{
    public class HomeController : Controller
    {
   
        public gwork _unitOfWork = new gwork();
        perfumeShopEntities ctx = new perfumeShopEntities();
        public ActionResult Index(string search)
        {
            menuModel model = new menuModel();
            return View(model.CreateModel(search));
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
        public ActionResult Productdetails(string search)
        {
            menuModel model = new menuModel();
            return View(model.CreateModel(search));
            //return View();

        }
        //public ActionResult AddToCart(int productId)
        //{
        //    if (Session["cart"] == null)
        //    {
        //        List<Item> cart = new List<Item>();
        //        var product = ctx.fume_Product.Find(productId);
        //        cart.Add(new Item()
        //        {
        //            Product = product,
        //            Quantity = 1
        //        });
        //        Session["cart"] = cart;
        //    }
        //    else
        //    {
        //        List<Item> cart = (List<Item>)Session["cart"];
        //        var product = ctx.fume_Product.Find(productId);
        //        foreach (var item in cart)
        //        {
        //            if (item.Product.ProductId == productId)
        //            {
        //                int prevQty = item.Quantity;
        //                cart.Remove(item);
        //                cart.Add(new Item()
        //                {
        //                    Product = product,
        //                    Quantity = prevQty + 1
        //                });
        //                break;
        //            }
        //            else
        //            {
        //                cart.Add(new Item()
        //                {
        //                    Product = product,
        //                    Quantity = 1
        //                });
        //            }
        //        }
        //        Session["cart"] = cart;
        //    }
        //    return Redirect("Index");
        //}
        public ActionResult AddToCart(int productId)
        {
            if (Session["cart"] == null)
            {
                List<Item> cart = new List<Item>();
                var product = ctx.fume_Product.Find(productId);
                cart.Add(new Item()
                {
                    Product = product,
                    Quantity = 1
                });
                Session["cart"] = cart;
            }
            else
            {
                List<Item> cart = (List<Item>)Session["cart"];
                var product = ctx.fume_Product.Find(productId);
                cart.Add(new Item()
                {
                    Product = product,
                    Quantity = 1
                });
                Session["cart"] = cart;
            }
            return Redirect("Index");
            

        }
        public ActionResult RemoveFromCart(int productId)
        {
            List<Item> cart = (List<Item>)Session["cart"];
            foreach (var item in cart)
            {
                if (item.Product.ProductId == productId)
                {
                    cart.Remove(item);
                    break;
                }
            }
            Session["cart"] = cart;
            return Redirect("Index");
           
        }

        public ActionResult Checkout()
        {
            return View();
        }




        public ActionResult CheckoutDetails()
        {
            return View();
        }

        public ActionResult info(int productId)
        {
            return View();

        }





    }
    



}