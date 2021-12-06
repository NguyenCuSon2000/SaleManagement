using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.DTO;
using Interfaces;
using Newtonsoft.Json;

namespace AnnaShop.Controllers
{
    //[Authorize]
    public class CartController : Controller
    {
        private readonly IProduct product;
        private readonly ICart cart;
        private readonly IUser user;
        public CartController(IProduct product, ICart cart, IUser user)
        {
            this.product = product;
            this.cart = cart;
            this.user = user;
        }
        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Thêm sản phẩm vào giỏ hàng
        /// </summary>
        /// <param name="pro">Sản phẩm model </param>
        /// <returns></returns>
        public JsonResult Buy(ProductDTO pro)
        {
            ProductDTO productModel = product.GetSingle(pro.Id);
            if (Session["cart"] == null) // nếu giỏ hàng trống
            {
                List<DetailOrderModel> cart = new List<DetailOrderModel>();
                DetailOrderModel ct = new DetailOrderModel();
                ct.Product = productModel;
                ct.Quantity = pro.Quantity;
                ct.Price = productModel.Price * ct.Quantity;
                cart.Add(ct);
                Session["cart"] = cart;
                return Json(cart, JsonRequestBehavior.AllowGet);
            }
            else // nếu giỏ hàng có chứa sp
            {
                List<DetailOrderModel> cart = (List<DetailOrderModel>)Session["cart"];
                int index = isExist(pro.Id);
                if (index != -1) // nếu sp thêm vào đã có trong giỏ hàng
                {
                    cart[index].Quantity += pro.Quantity;
                }
                else // nếu sản phẩm thêm vào chưa có trong giỏ hàng
                {
                    DetailOrderModel ct = new DetailOrderModel();
                    ct.Product = productModel;
                    ct.Quantity = pro.Quantity;
                    ct.Price = productModel.Price * ct.Quantity;
                    cart.Add(ct);
                }
                Session["cart"] = cart;
                return Json(cart, JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// kiểm tra sản phẩm có trong session Cart hay ko
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private int isExist(int id)
        {
            List<DetailOrderModel> cart = (List<DetailOrderModel>)Session["cart"];
            for (int i = 0; i < cart.Count; i++)
                if (cart[i].Product.Id.Equals(id))
                    return i;
            return -1;
        }

        public JsonResult getCart()
        {
            int sl = 0;
            double thanhtien = 0;
            List<DetailOrderModel> ds = new List<DetailOrderModel>();
            if (Session["Cart"] == null)
            {
                Session["Cart"] = new List<DetailOrderModel>();
                sl = 0;
                thanhtien = 0;
            }
            else
            {
                ds = Session["Cart"] as List<DetailOrderModel>;
                thanhtien = Convert.ToDouble(ds.Sum(s => s.Price));
                sl = ds.Sum(s => s.Quantity);
            }
            return Json(new { dsGH = ds, soluong = sl, Thanhtien = thanhtien }, JsonRequestBehavior.AllowGet);
        }

        public string Remove(ProductDTO model)
        {
            List<DetailOrderModel> cart = (List<DetailOrderModel>)Session["Cart"];
            int index = isExist(model.Id);
            cart.RemoveAt(index);
            Session["cart"] = cart;
            return "Delete Successfully";
        }

        #region Mua hang
        public string Order(UserModel us)
        {
            double total = 0;
            List<DetailOrderModel> ds = Session["Cart"] as List<DetailOrderModel>;
            for (int i = 0; i < ds.Count; i++)
            {
                total += ds[i].Price;
            }
            OrderFormModel dh = new OrderFormModel();
            dh.DateCreate = DateTime.Now;
            dh.User = us;
            dh.Note = "";
            dh.Phone = us.Phone;
            dh.Address = us.Address; //địa chỉ user nhập
            dh.DateShip = DateTime.Now.AddDays(5);
            dh.Price = total;
            cart.addOrder(dh);
            OrderFormModel donHang = cart.addOrder(dh);

            // add orderDetail
            for (int i = 0; i < ds.Count; i++)
            {
                DetailOrderModel ctdh = new DetailOrderModel();
                ctdh.Order = donHang;
                ctdh.Product = ds[i].Product;
                ctdh.Price = ds[i].Price;
                ctdh.Quantity = ds[i].Quantity;
                cart.addOrderDetail(ctdh);
            }
            ds.Clear();
            return "Đặt hàng thành công. Cảm ơn bạn đã tin tưởng Annie Shop!";
        }
        #endregion

        #region history
        public ActionResult History()
        {
            return View();
        }
        public JsonResult SpBuy()
        {
            string tentk = Request.Cookies["LoginDetail"].Values["Username"].ToString();
            string mk = Request.Cookies["LoginDetail"].Values["Password"].ToString();
            // lấy các chi tiết đơn đã mua của user đang login
            UserModel u = user.GetSingle(tentk, mk);
            var listCT = new List<DetailOrderModel>();
            listCT = cart.spDaMua(u);

            return Json(listCT, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}