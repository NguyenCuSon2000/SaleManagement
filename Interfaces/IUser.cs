using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DTO;

namespace Interfaces
{
    public interface IUser
    {
        Model.DTO.UserModel GetSingle(string uid, string pwd);
        bool CheckUser(string uid, string pwd);
        void AddUser(Model.DTO.UserModel model);
        int AddUserForFb(Model.DTO.UserModel model);
    }
}
