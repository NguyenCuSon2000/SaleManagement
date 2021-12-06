using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
    public class DetailOrderModel
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public OrderFormModel Order { get; set; }
        public ProductDTO Product { get; set; }

        public String Date { get; set; }
    }
}
