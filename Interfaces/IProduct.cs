using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.DTO;

namespace Interfaces
{
    public interface IProduct
    {
        List<Model.DTO.ProductDTO> GetAll();

        List<Model.DTO.ProductDTO> GetNew();
        List<ProductDTO> GetProductByCategory(int maLoai);
        Model.DTO.ProductDTO GetSingle(int id);
        Model.DTO.ProductDTO UpdateProduct(Model.DTO.ProductDTO sanPham);
        bool DeleteProduct(Model.DTO.ProductDTO model);
        void InsertProduct(ProductDTO model);
    }
}
