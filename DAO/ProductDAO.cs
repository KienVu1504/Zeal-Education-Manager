using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZealEducationManager.Model;

namespace ZealEducationManager.DAO
{
    public class ProductDAO
    {
        ZealEducationManagerDbContext db = null;
        public ProductDAO()
        {
            db = new ZealEducationManagerDbContext();
        }
        public IEnumerable<Product> ListAllPaging(string searchString, int page, int pagesize)
        {
            IQueryable<Product> model = db.Products;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString) || x.MetaTitle.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreateDate).ToPagedList(page, pagesize);
        }
        public bool Delete(int Id)
        {
            try
            {
                var product = db.Products.Find(Id);
                db.Products.Remove(product);
                db.SaveChanges();
                return true;
            }
            catch (Exception) { return false; }
        }
        public long Insert(Product entity)
        {
            db.Products.Add(entity);
            db.SaveChanges();
            return entity.Id;
        }
    }
}