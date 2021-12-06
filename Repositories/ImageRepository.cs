using Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using Model;

namespace Repositories
{
    public class ImageRepository : Interfaces.IImage
    {
        private readonly DataAccess.ShopAnnaContainer _context = new DataAccess.ShopAnnaContainer();

        public List<ImageModel> GetAll(ProductDTO model)
        {
            var list = _context.Images.Where(x => x.Product.Id == model.Id).ToList();
            return Mapper.ImageMap(list);
        }
    }
}
