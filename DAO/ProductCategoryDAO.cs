using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZealEducationManager.Model;

namespace ZealEducationManager.DAO
{
    public class ProductCategoryDAO
    {
        ZealEducationManagerDbContext db = null;
        public ProductCategoryDAO()
        {
            db = new ZealEducationManagerDbContext();
        }
        public List<ProductCategory> ListAll()
        {
            return db.ProductCategories.Where(c => c.Status == true).OrderBy(c => c.DisplayOrder).ToList();
        }
        public ProductCategory ViewDetail(int id)
        {
            return db.ProductCategories.Find(id);
        }
    }
}