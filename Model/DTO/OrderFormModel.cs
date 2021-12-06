using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
    public class OrderFormModel
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public double Price { get; set; }
        public string Note { get; set; }
        public bool State { get; set; }
        public System.DateTime DateCreate { get; set; }
        public System.DateTime DateShip { get; set; }
        public UserModel User { get; set; }

        public int Quantity {get; set;}

    }
}
