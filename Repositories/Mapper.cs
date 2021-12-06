using Model;
using Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class Mapper
    {
        #region Category
        public static List<CategoryModel> CategoryMap(List<ProductCategory> listEntity)
        {
            var r = new List<CategoryModel>();
            foreach (var item in listEntity)
            {
                r.Add(CategoryMap(item));
            }
            return r;
        }
        public static CategoryModel CategoryMap(ProductCategory entity)
        {
            var r = new CategoryModel();
            r.Id = entity.Id;
            r.Name = entity.Name;
            return r;
        }
        #endregion
        #region Detail order
        public static List<DetailOrderModel> DetailOrderMap(List<DetailOrder> listEntity)
        {
            var r = new List<DetailOrderModel>();
            foreach (var item in listEntity)
            {
                r.Add(DetailOrderMap(item));
            }
            return r;
        }
        public static DetailOrderModel DetailOrderMap(DetailOrder entity)
        {
            var r = new DetailOrderModel();
            r.Id = entity.Id;
            r.Price = entity.Price;
            r.Quantity = entity.Quantity;
            r.Order = entity.OderForm == null ? null : OrderMap(entity.OderForm);
            r.Product = entity.Product == null ? null : ProductMap(entity.Product);
            return r;
        }
        #endregion
        #region Order
        public static List<OrderFormModel> OrderMap(List<OderForm> listEntity)
        {
            var r = new List<OrderFormModel>();
            foreach (var item in listEntity)
            {
                r.Add(OrderMap(item));
            }
            return r;
        }
        public static OrderFormModel OrderMap(OderForm entity)
        {
            var r = new OrderFormModel();
            r.Id = entity.Id;
            r.Address = entity.Address;
            r.DateCreate = entity.DateCreate;
            r.DateShip = entity.DateShip;
            r.Note = entity.Note;
            r.Phone = entity.Phone;
            r.Price = entity.Price;
            r.State = entity.State;
            r.User = entity.User == null ? null : UserMap(entity.User);
            return r;
        }
        #endregion
        #region User
        public static List<UserModel> UserMap(List<User> listEntity)
        {
            var r = new List<UserModel>();
            foreach (var item in listEntity)
            {
                r.Add(UserMap(item));
            }
            return r;
        }
        public static UserModel UserMap(User entity)
        {
            var r = new UserModel();
            r.Id = entity.Id;
            r.Name = entity.Name;
            r.UserName = entity.UserName;
            r.PassWord = entity.PassWord;
            r.Image = entity.Image;
            r.Phone = entity.Phone;
            r.Address = entity.Address;
            r.DateOfBirth = entity.DateOfBirth;
            r.Email = entity.Email;
            return r;
        }
        #endregion
        #region Product
        public static ProductDTO ProductMap(Product entity)
        {
            var r = new ProductDTO();
            r.Id = entity.Id;
            r.Name = entity.Name;
            r.Price = entity.Price;
            r.Color = entity.Color;
            r.Quantity = entity.Quantity;
            r.Size = entity.Size;
            r.State = entity.State;
            r.AvatarImage = entity.AvatarImage;
            r.Description = entity.Description;
            r.Category = entity.ProductCategory == null ? null : CategoryMap(entity.ProductCategory);
            return r;
        }
        public static List<ProductDTO> ProductMap(List<Product> listEntity)
        {
            var r = new List<ProductDTO>();
            foreach (var item in listEntity)
            {
                r.Add(ProductMap(item));
            }
            return r;
        }
        #endregion
        #region Slide
        public static SlideModel SlideMap(Slide entity)
        {
            var r = new SlideModel();
            r.Id = entity.Id;
            r.Image = entity.Image;
            r.Link = entity.Link;
            r.Heading = entity.Heading;
            return r;
        }
        public static List<SlideModel> SlideMap(List<Slide> listEntity)
        {
            var r = new List<SlideModel>();
            foreach (var item in listEntity)
            {
                r.Add(SlideMap(item));
            }
            return r;
        }
        #endregion
        #region Image
        public static ImageModel ImageMap(Image entity)
        {
            var r = new ImageModel();
            r.Id = entity.Id;
            r.Img = entity.Img;
            r.Product = entity.Product == null ? null : ProductMap(entity.Product);
            return r;
        }
        public static List<ImageModel> ImageMap(List<Image> listEntity)
        {
            var r = new List<ImageModel>();
            foreach (var item in listEntity)
            {
                r.Add(ImageMap(item));
            }
            return r;
        }
        #endregion
    }
}
