using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.DTO;

namespace Interfaces
{
    public interface IImage
    {
        List<Model.DTO.ImageModel> GetAll(ProductDTO model);
    }
}
