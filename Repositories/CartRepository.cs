using DataAccess;
using Model;
using Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class CartRepository : Interfaces.ICart
    {
        private readonly DataAccess.ShopAnnaContainer _context = new ShopAnnaContainer();

        public OrderFormModel addOrder(OrderFormModel dh)
        {
            OderForm hoaDon = new OderForm()
            {
                Id = dh.Id,
                User = _context.Users.FirstOrDefault(x => x.Id == dh.User.Id),
                Address = dh.Address,
                DateCreate = dh.DateCreate,
                DateShip = dh.DateShip,
                Note = dh.Note,
                Phone = dh.Phone,
                Price = dh.Price,
                State = true,
            };
            _context.OderForms.Add(hoaDon);
            _context.SaveChanges();
            return Mapper.OrderMap(hoaDon);
        }

        public void addOrderDetail(DetailOrderModel ctdh)
        {
            DetailOrder ct = new DetailOrder()
            {
                Id = ctdh.Id,
                OderForm = _context.OderForms.FirstOrDefault(x => x.Id == ctdh.Order.Id),
                Price = ctdh.Price,
                Product =_context.Products.FirstOrDefault(x=> x.Id == ctdh.Product.Id),
                Quantity = ctdh.Quantity,
            };
            _context.DetailOrders.Add(ct);
            _context.SaveChanges();

        }

        public List<OrderFormModel> getAll()
        {
            var ds = Mapper.OrderMap(_context.OderForms.ToList());
            return ds;
        }

        public OrderFormModel getSingle(OrderFormModel obj)
        {
            return Mapper.OrderMap(_context.OderForms.FirstOrDefault(x => x.Id == obj.Id));
        }

        //Chi tiết đơn hàng
        public List<Model.DTO.DetailOrderModel> GetDetailOrderByOrder(int OderForm_Id)
        {
            var list = _context.DetailOrders.Where(x => x.OderForm.Id == OderForm_Id).ToList();
            return Mapper.DetailOrderMap(list);
        }

        public List<Model.DTO.DetailOrderModel> spDaMua(UserModel user)
        {
            var hd = Mapper.OrderMap(_context.OderForms.ToList());
            var listHD = hd.FindAll(x => x.User.Id == user.Id);
            var listCT = Mapper.DetailOrderMap(_context.DetailOrders.ToList());
            var result = new List<Model.DTO.DetailOrderModel>();
            foreach(var i in listHD)
            {
                //laays ra cthd co hd = i
                var a = listCT.FindAll(x => x.Order.Id == i.Id);
                foreach(var j in a)
                {
                    j.Date = i.DateCreate.ToString();
                    result.Add(j);
                }
            }
            return result;
        }

    }
}
