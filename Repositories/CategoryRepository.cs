using DataAccess;
using Interfaces;
using Model;
using Model.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class CategoryRepository : Interfaces.ICategory
    {
        private readonly DataAccess.ShopAnnaContainer _context = new ShopAnnaContainer();

        public List<Model.DTO.CategoryModel> GetAll()
        {
            var listEntity = _context.ProductCategories.ToList();
            return Mapper.CategoryMap(listEntity);
        }

        public CategoryModel GetSingle(int id)
        {
            var entity = _context.ProductCategories.FirstOrDefault(x => x.Id == id);
            return Mapper.CategoryMap(entity);
        }

        public void InsertCategory(CategoryModel model)
        {
            ProductCategory lsp = new ProductCategory() { Id = model.Id, Name = model.Name };
            _context.ProductCategories.Add(lsp);
            _context.SaveChanges();
        }

        public CategoryModel UpdateCategory(CategoryModel model)
        {
            ProductCategory lsp = _context.ProductCategories.FirstOrDefault(x => x.Id == model.Id);
            if (lsp != null)
            {
                lsp.Name = model.Name;
                _context.SaveChanges();
            }
            return model;
        }
        public bool Delete(CategoryModel model)
        {
            if (_context.ProductCategories.FirstOrDefault(x => x.Id == model.Id) != null)
            {
                ProductCategory lsp = _context.ProductCategories.FirstOrDefault(x => x.Id == model.Id);
                _context.ProductCategories.Remove(lsp);
                _context.SaveChanges();
                return true;
            }
            else
                return false;
        }
    }
}
