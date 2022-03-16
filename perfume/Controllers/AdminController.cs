using Newtonsoft.Json;
using perfume.fume;
using perfume.Models;
using perfume.reposit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace perfume.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public gwork _unitOfWork=new gwork();
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
    }
}
