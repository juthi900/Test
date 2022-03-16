using perfume.fume;
using perfume.reposit;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace perfume.Models.Homemenu
{
    public class menuModel
    {
        public gwork _unitOfWork = new gwork();
        perfumeShopEntities context = new perfumeShopEntities();

        public IEnumerable<fume_Product> ListOfProducts { get; set; }
        public menuModel CreateModel(string search)
        {
            SqlParameter[] pram = new SqlParameter[] {
                   new SqlParameter("@search",search??(object)DBNull.Value)
                   };
            IEnumerable<fume_Product> data = context.Database.SqlQuery<fume_Product>("GetBySearch @search", pram).ToList();
            //IPagedList<fume_Product> data = context.Database.SqlQuery<fume_Product>("GetBySearch @search", pram).ToList().ToPagedList(page ?? 1, pageSize);
            return new menuModel
            {
                ListOfProducts = data
        };


    }
    }
}
