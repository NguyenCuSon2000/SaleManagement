using Interfaces;
using Model.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AnnaShop.Areas.Admin.Controllers
{
    public class AdminProductController : Controller
    {
        private readonly IProduct product;
        private readonly IUser user;
        public AdminProductController(IProduct product, IUser user)
        {
            this.product = product;
            this.user = user;
        }
        // GET: Admin/Product
        public ActionResult Index()
        {
            if (Request.Cookies["LoginDetail"] == null || String.Compare(Request.Cookies["LoginDetail"].Values["Username"].ToString(), "annie", true) != 0)
            {
                //chuyển hướng
                Response.Redirect("/Login/Index");
            }
            return View();
        }
        public ActionResult UploadFile()
        {
            string saveToDbPath = "";
            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];
                if (file != null && file.ContentLength > 0)
                {
                    string fileNameWithoutExt = Path.GetFileNameWithoutExtension(file.FileName);
                    string fileNameExt = Path.GetExtension(file.FileName);
                    string fileName = fileNameWithoutExt + "-" + Guid.NewGuid().ToString() + fileNameExt;
                    string path = Path.Combine(Server.MapPath("~/Templatess/images/Products/"), fileName);
                    saveToDbPath = "/Templatess/images/Products/" + fileName;

                    string fullPath = Server.MapPath(saveToDbPath);
                    file.SaveAs(fullPath);
                }
            }
            return Json(new { filePath = saveToDbPath });
        }
        /// <summary>
        /// Get all product
        /// </summary>
        /// <returns></returns>
        public JsonResult GetAll_Product()
        {
            var data = product.GetAll();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Get single product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult Get_productById(int id)
        {
            var data = product.GetSingle(id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// insert a product
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string Insert_product(ProductDTO model)
        {
            if (model != null)
            {
                product.InsertProduct(model);
                return "Add successfl";
            }
            else return "Product not inserted. Try again!";
        }
        /// <summary>
        /// delete product
        /// </summary>
        /// <param name="sp"></param>
        /// <returns></returns>
        public string Delete_product(ProductDTO sp)
        {
            if (sp != null)
            {
                product.DeleteProduct(sp);
                return "product Deleted Successfully";
            }
            else return "product Not Deleted! Try Again";
        }
        /// <summary>
        /// update product infomation
        /// </summary>
        /// <param name="sp"></param>
        /// <returns></returns>
        public string Update_product(ProductDTO sp)
        {
            if (sp != null)
            {
                product.UpdateProduct(sp);
                return "product Updated Successfully";
            }
            else
            {
                return "product Not Updated! Try Again";
            }
        }
    }
}