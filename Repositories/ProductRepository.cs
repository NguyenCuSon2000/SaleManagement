using DataAccess;
using Model;
using Model.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class ProductRepository : Interfaces.IProduct
    {
        private readonly ShopAnnaContainer _context = new ShopAnnaContainer();

        public bool DeleteProduct(ProductDTO model)
        {
            if (_context.Products.FirstOrDefault(x => x.Id == model.Id) != null)
            {
                Product sp = _context.Products.FirstOrDefault(x => x.Id == model.Id);
                _context.Products.Remove(sp);
                _context.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public List<Model.DTO.ProductDTO> GetAll()
        {
            var list = _context.Products.ToList();
            return Mapper.ProductMap(list);
        }

        public List<ProductDTO> GetNew()
        {
            var list = _context.Products.OrderByDescending(x => x.Id).Take(8).ToList();
            return Mapper.ProductMap(list);
        }

        public List<ProductDTO> GetProductByCategory(int maLoai)
        {
            var list = _context.Products.Where(x => x.ProductCategory.Id == maLoai).ToList();
            return Mapper.ProductMap(list);
        }

        public ProductDTO GetSingle(int id)
        {
            var sp = _context.Products.FirstOrDefault(x => x.Id == id);
            return Mapper.ProductMap(sp);
        }

        public void InsertProduct(ProductDTO model)
        {
            Product sp = new Product() {
                Id = model.Id,
                Name = model.Name,
                Color = model.Color,
                Description = model.Description,
                Size = model.Size,
                Price = model.Price,
                Quantity = model.Quantity,
                State = true,
                AvatarImage = model.AvatarImage,
                ProductCategory = _context.ProductCategories.FirstOrDefault(x=>x.Id == model.Category.Id),
            };
            _context.Products.Add(sp);
            _context.SaveChanges();
        }

        public ProductDTO UpdateProduct(ProductDTO model)
        {
            Product sp = _context.Products.FirstOrDefault(x => x.Id == model.Id);
            if (sp != null)
            {
                sp.Name = model.Name;
                sp.Color = model.Color;
                sp.Description = model.Description;
                sp.Size = model.Size;
                sp.Price = model.Price;
                sp.Quantity = model.Quantity;
                sp.State = model.State;
                sp.AvatarImage = model.AvatarImage;
                sp.ProductCategory = _context.ProductCategories.FirstOrDefault(x => x.Id == model.Category.Id);
                _context.SaveChanges();
            }
            return model;
        }
    }
}
