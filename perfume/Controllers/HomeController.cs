using perfume.fume;
using perfume.Models.Homemenu;
using perfume.reposit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using perfume.ViewModel;
using System.Web.Security;

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

        //SignUp & Login

        public ActionResult Success()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        //HTTP is designed to send and receive the data between client and server using web pages.
        [HttpPost]
        //This object contains all the values entered in the form by the user.
        public ActionResult SaveRegisterDetails(Register registerDetails)
        {

            if (ModelState.IsValid)
            {
                //create database context using Entity framework 
                using (var databaseContext = new perfumeShopEntities())
                {
                    //If the model state is valid i.e. the form values passed the validation then we are storing the User's details in DB.
                    RegisterUser reglog = new RegisterUser();

                    //Save all details in RegitserUser object
                    reglog.FirstName = registerDetails.FirstName;
                    reglog.LastName = registerDetails.LastName;
                    reglog.Email = registerDetails.Email;
                    reglog.Password = registerDetails.Password;


                    //Calling the SaveDetails method which saves the details.
                    databaseContext.RegisterUsers.Add(reglog);
                    databaseContext.SaveChanges();
                }

                ViewBag.Message = "User Details Saved";
                return RedirectToAction("Login");
                //return View("Register");
            }
            else
            {


                return View("Register", registerDetails);
            }
        }


        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {

                var isValidUser = IsValidUser(model);

                if (isValidUser != null)
                {
                    FormsAuthentication.SetAuthCookie(model.Email, false);
                    return RedirectToAction("Productdetails");
                }
                else
                {
                    ModelState.AddModelError("Failure", "Wrong Username and password combination !");
                    return View();
                }
            }
            else
            {

                return View(model);
            }
        }


        //function to check if User is valid or not
        public RegisterUser IsValidUser(LoginViewModel model)
        {
            using (var dataContext = new perfumeShopEntities())
            {

                RegisterUser user = dataContext.RegisterUsers.Where(query => query.Email.Equals(model.Email) && query.Password.Equals(model.Password)).SingleOrDefault();
                if (user == null)
                    return null;
                else
                    return user;
            }
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index");
        }


        //contact





        public ActionResult Contact1()
        {
            return View();
        }


        [HttpPost]
        public ActionResult SaveRegisterDetails2(Contact1 registerDetails2)
        {

            if (ModelState.IsValid)
            {

                using (var databaseContext = new perfumeShopEntities())
                {

                    Contact reglog = new Contact();



                    reglog.Name = registerDetails2.Name;

                    reglog.Email = registerDetails2.Email;
                    reglog.details = registerDetails2.Details;

                    databaseContext.Contacts.Add(reglog);
                    databaseContext.SaveChanges();
                }

                ViewBag.Message = "User Details Saved";
                return View("Contact1");
            }
            else
            {

                return View("Contact1", registerDetails2);
            }
        }









    }
    



}