using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZealEducationManager.Common;
using ZealEducationManager.DAO;
using ZealEducationManager.Model;

namespace ZealEducationManager.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        // GET: Admin/Product
        public ActionResult Index(string searchString, int page = 1, int pageSize = 200)
        {
            var dao = new ProductDAO();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            SetViewBag();
            return View(model);
        }
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new ProductDAO().Delete(id);
            return RedirectToAction("Index");
        }
        public void SetViewBag(long? selectedId = null)
        {
            var dao = new ProductCategoryDAO();
            ViewBag.CategoryId = dao.ListAll();
        }
        [HttpPost]
        public JsonResult AddProductAjax(string name, string code, string metaTitle, string description, string image, string categoryId, string detail, string listType, string listFile)
        {
            try
            {
                var dao = new ProductDAO();
                Product product = new Product();
                product.Name = name;
                product.CreateDate = DateTime.Now;
                product.Code = code;
                product.MetaTitle = metaTitle;
                product.Description = description;
                product.Image = image;
                product.CategoryId = Convert.ToInt16(categoryId);
                product.Status = true;
                product.Detail = detail;
                product.ListType = listType;
                product.ListFile = listFile;

                long id = dao.Insert(product);
                if (id > 0)
                {
                    return Json(new { status = true });
                }
                else
                {
                    return Json(new { status = false });
                }
            }
            catch (Exception)
            {
                return Json(new
                {
                    status = false
                });
            }
        }
    }
}