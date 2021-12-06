using Interfaces;
using Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AnnaShop.Areas.Admin.Controllers
{
    public class OrderDetailsController : Controller
    {
        private readonly ICart cart;
        public OrderDetailsController(ICart cart)
        {
            this.cart = cart;
        }
        //GET: Admin/OrderDetails
        public ActionResult Index(int OderForm_Id)
        {
            Session.Add("OderForm_Id", OderForm_Id);
            return View();
        }

        public JsonResult GetDetailOrderByOrder()
        {
            List <Model.DTO.DetailOrderModel> data = new List<Model.DTO.DetailOrderModel>();
            try
            {
                data = cart.GetDetailOrderByOrder(Convert.ToInt32(Session["OderForm_Id"].ToString()));
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                throw ex;
            }
          
     
        }
    }
}