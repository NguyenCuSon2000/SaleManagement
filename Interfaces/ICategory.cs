using Model;
using Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface ICategory
    {
        List<Model.DTO.CategoryModel> GetAll();
        Model.DTO.CategoryModel GetSingle(int id);
        bool Delete(CategoryModel model);
        void InsertCategory(CategoryModel model);
        CategoryModel UpdateCategory(CategoryModel model);
    }
}
