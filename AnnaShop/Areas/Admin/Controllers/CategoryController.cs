using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
using Interfaces;
using Model.DTO;

namespace AnnaShop.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategory category;
        public CategoryController(ICategory category)
        {
            this.category = category;
        }

        // GET: Admin/Category
        public ActionResult Index()
        {
            if (Request.Cookies["LoginDetail"] == null || String.Compare(Request.Cookies["LoginDetail"].Values["Username"].ToString(), "annie", true) != 0)
            {
                //chuyển hướng
                Response.Redirect("/Login/Index");
            }
            return View();
        }
        /// <summary>
        /// Get all category
        /// </summary>
        /// <returns></returns>
        public JsonResult GetAll_Category()
        {
            var data = category.GetAll();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Get single category
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult Get_CategoryById(int id)
        {
            var data = category.GetSingle(id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// insert a product category
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string Insert_Category(CategoryModel model)
        {
            if (model != null)
            {
                category.InsertCategory(model);
                return "Add successful";
            }
            else return "Product not inserted. Try again!";
        }
        /// <summary>
        /// delete product category
        /// </summary>
        /// <param name="LSP"></param>
        /// <returns></returns>
        public string Delete_Category(CategoryModel LSP)
        {
            if (LSP != null)
            {
                category.Delete(LSP);
                return "Category Deleted Successfully";
            }
            else return "Category Not Deleted! Try Again";
        }
        /// <summary>
        /// update category infomation
        /// </summary>
        /// <param name="lsp"></param>
        /// <returns></returns>
        public string Update_Category(CategoryModel lsp)
        {
            if (lsp != null)
            {
                category.UpdateCategory(lsp);
                return "Category Updated Successfully";
            }
            else
            {
                return "Category Not Updated! Try Again";
            }
        }
    }
}