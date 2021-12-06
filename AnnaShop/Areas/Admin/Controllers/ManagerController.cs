using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AnnaShop.Areas.Admin.Controllers
{
    public class ManagerController : Controller
    {
        private readonly ShopAnnaContainer _context = new ShopAnnaContainer();
        // GET: Admin/Manager
        public ActionResult Index()
        {
            if (Request.Cookies["LoginDetail"] == null || String.Compare(Request.Cookies["LoginDetail"].Values["Username"].ToString(), "annie", true) != 0)
            {
                //chuyển hướng
                Response.Redirect("/Login/Index");
            }
            return View();
        }
        public JsonResult getDoanhThuThang()
        {
            var a = _context.OderForms.ToList();
            var dshd = Repositories.Mapper.OrderMap(a);
            var listDT = new List<double>();
            for (int i = 1; i < 13; i++)
            {
                double total = 0;
                var c = dshd.FindAll(p => p.DateCreate.Month == i); //danh sach  hoa don co thang = thang dang duyet
                var sl = c.Count(); //sl hoa don trong thang
                total += c.Sum(p => p.Price); // tong doanh thu thang
                listDT.Add(total);
            }
            double max = listDT[0];
            foreach (var i in listDT)
            {
                if (max < i) max = i;
            }
            return Json(new { Dts = listDT, dtMax = max }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getDtLoaiSp()
        {
            var dsct = Repositories.Mapper.DetailOrderMap(_context.DetailOrders.ToList());
            var loai = Repositories.Mapper.CategoryMap(_context.ProductCategories.ToList());
            double tong = 0;
            var dslsp = new List<string>();
            var dspt = new List<double>();
            if(dsct.Count > 0)
            {
                foreach (var i in loai)
                {
                    var danhSachSanPham = dsct.FindAll(p => p.Product.Category.Id == i.Id); //ds cthd có sản phẩm đã bán = lsp đang duyệt
                    tong += danhSachSanPham.Count();
                }
                foreach (var i in loai)
                {
                    var danhSachSanPham = dsct.FindAll(p => p.Product.Category.Id == i.Id);
                    i.Percent = ((double)danhSachSanPham.Count() / tong) * 100 ;
                    dslsp.Add(i.Name);
                    dspt.Add(Math.Round(i.Percent, 2));
                }
            }
            return Json(new { dsLSP = dslsp, dsPT = dspt }, JsonRequestBehavior.AllowGet);
        }

    }
}