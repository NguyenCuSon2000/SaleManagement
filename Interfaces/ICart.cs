using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface ICart
    {
        Model.DTO.OrderFormModel addOrder(Model.DTO.OrderFormModel dh);
        void addOrderDetail(Model.DTO.DetailOrderModel ctdh);
        List<Model.DTO.DetailOrderModel> spDaMua(Model.DTO.UserModel user);
        List<Model.DTO.OrderFormModel> getAll();
        Model.DTO.OrderFormModel getSingle(Model.DTO.OrderFormModel obj);

        List<Model.DTO.DetailOrderModel> GetDetailOrderByOrder(int mahd);
    }
}
