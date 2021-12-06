using Model.DTO;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;
using Model;

namespace Repositories
{
    public class UserRepository : Interfaces.IUser
    {
        private readonly ShopAnnaContainer _context = new ShopAnnaContainer();

        public void AddUser(UserModel model)
        {
            User us = new User()
            {
                Id = model.Id,
                DateOfBirth = DateTime.Today,
                Name = model.Name,
                Email = model.Email,
                UserName = model.UserName,
                PassWord = model.PassWord,
                Phone = model.Phone
            };
            _context.Users.Add(us);
            _context.SaveChanges();
        }

        public int AddUserForFb(UserModel model)
        {
            var u = _context.Users.SingleOrDefault(x => x.UserName == model.UserName);
            if(u == null)
            {
                User us = new User()
                {
                    Id = model.Id,
                    DateOfBirth = DateTime.Today,
                    Name = model.Name,
                    Email = model.Email,
                    UserName = model.UserName,
                    PassWord = model.PassWord,
                    Phone = model.Phone,
                };
                _context.Users.Add(us);
                _context.SaveChanges();
                return us.Id;
            }
            else
            {
                return u.Id;
            }
        }

        public bool CheckUser(string uid, string pwd)
        {
            Model.User u = _context.Users.FirstOrDefault(x => x.PassWord == pwd && x.UserName == uid);
            if (u != null) return true;
            else return false;
        }

        public UserModel GetSingle(string uid, string pwd)
        {
            Model.User u = _context.Users.FirstOrDefault(x => x.PassWord == pwd && x.UserName == uid);
            return Mapper.UserMap(u);
        }
    }
}
