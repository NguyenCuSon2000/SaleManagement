using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public bool State { get; set; }
        public string Description { get; set; }
        public string AvatarImage { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }

        public CategoryModel Category { get; set; }

    }
}
