using Newtonsoft.Json;
using perfume.fume;
using perfume.Models;
using perfume.reposit;
using perfume.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace perfume.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public gwork _unitOfWork = new gwork();
        //public List<SelectListItem> GetCategory()
        //{
        //    List<SelectListItem> list = new List<SelectListItem>();
        //    var cat = _unitOfWork.GetRepositoryInstance<fume_Category>().GetAllRecords();
        //    foreach (var item in cat)
        //    {
        //        list.Add(new SelectListItem { Value = item.CategoryId.ToString(), Text = item.CategoryName });
        //    }
        //    return list;
        //}
        //public ActionResult Dashboard()
        //{
        //    return View();
        //}
        //public ActionResult Categories()
        //{
        //    List<fume_Category> allcategories = _unitOfWork.GetRepositoryInstance<fume_Category>().GetAllRecordsIQueryable().Where(i => i.IsDelete == false).ToList();
        //    return View(allcategories);
        //}
        //public ActionResult AddCategory()
        //{
        //    //ViewBag.CategoryList = GetCategory();

        //    return UpdateCategory(0);
        //}

        //public ActionResult UpdateCategory(int categoryId=0)
        //{
        //    ViewBag.CategoryList = GetCategory();
        //    fumedetail cd;
        //    if (categoryId != 0)
        //    {
        //        cd = JsonConvert.DeserializeObject<fumedetail>(JsonConvert.SerializeObject(_unitOfWork.GetRepositoryInstance<fume_Category>().GetFirstorDefault(categoryId)));
        //    }
        //    else
        //    {
        //        cd = new fumedetail();
        //    }
        //    return View("UpdateCategory", cd);

        //}
        public ActionResult Product()
        {
            return View(_unitOfWork.GetRepositoryInstance<fume_Product>().GetProduct());
        }
        public ActionResult ProductEdit(int productId)
        {
            //ViewBag.CategoryList = GetCategory();
            return View(_unitOfWork.GetRepositoryInstance<fume_Product>().GetFirstorDefault(productId));
        }

        [HttpPost]
        public ActionResult ProductEdit(fume_Product tbl, HttpPostedFileBase file)
        {
            string pic = null;
            if (file != null)
            {
                pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/Perfumeimg/"), pic);
                // file is uploaded
                file.SaveAs(path);
            }
            tbl.ProductImage = file != null ? pic : tbl.ProductImage;


            _unitOfWork.GetRepositoryInstance<fume_Product>().Update(tbl);
            return RedirectToAction("Product");

        }
        //public ActionResult CategoryEdit(int catId)
        //{
        //    return View(_unitOfWork.GetRepositoryInstance<fume_Category>().GetFirstorDefault(catId));
        //}
        //[HttpPost]
        //public ActionResult CategoryEdit(fume_Category tbl)
        //{
        //    _unitOfWork.GetRepositoryInstance<fume_Category>().Update(tbl);
        //    return RedirectToAction("Categories");
        //}


        public ActionResult ProductAdd()
        {

            //ViewBag.CategoryList = GetCategory();
            return View();
        }
        [HttpPost]
        public ActionResult ProductAdd(fume_Product tbl, HttpPostedFileBase file)
        {
            string pic = null;
            if (file != null)
            {
                pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/img/"), pic);
                // file is uploaded
                file.SaveAs(path);
            }
            tbl.ProductImage = pic;
            tbl.CreatedDate = DateTime.Now;
            _unitOfWork.GetRepositoryInstance<fume_Product>().Add(tbl);
            return RedirectToAction("Product");
        }





        // here
        public ActionResult Register1()
        {
            return View();
        }

        //HTTP is designed to send and receive the data between client and server using web pages.
        [HttpPost]
        //This object contains all the values entered in the form by the user.
        public ActionResult SaveRegisterDetails2(Register1 registerDetails2)
        {

            if (ModelState.IsValid)
            {
                //create database context using Entity framework 
                using (var databaseContext = new perfumeShopEntities())
                {
                    //If the model state is valid i.e. the form values passed the validation then we are storing the User's details in DB.
                    Tbl_Admin reglog = new Tbl_Admin();
                    //Save all details in RegitserUser object
                    reglog.FirstName = registerDetails2.FirstName;
                    reglog.LastName = registerDetails2.LastName;
                    reglog.Email = registerDetails2.Email;
                    reglog.Password = registerDetails2.Password;


                    //Calling the SaveDetails method which saves the details.
                    databaseContext.Tbl_Admin.Add(reglog);
                    databaseContext.SaveChanges();
                }

                ViewBag.Message = "User Details Saved";
                return RedirectToAction("Login1");
                //return View("Register");
            }
            else
            {


                return View("Register1", registerDetails2);
            }
        }


        public ActionResult Login1()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login1(LoginViewModel1 model)
        {
            if (ModelState.IsValid)
            {

                var isValidUser = IsValidUser(model);

                if (isValidUser != null)
                {
                    FormsAuthentication.SetAuthCookie(model.Email, false);
                    return RedirectToAction("Product");
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
        public Tbl_Admin IsValidUser(LoginViewModel1 model)
        {
            using (var dataContext = new perfumeShopEntities())
            {

                Tbl_Admin user = dataContext.Tbl_Admin.Where(query => query.Email.Equals(model.Email) && query.Password.Equals(model.Password)).SingleOrDefault();
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
            return RedirectToAction("Register1");
        }





    }

}
