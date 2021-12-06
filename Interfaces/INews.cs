using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface INews
    {
        List<Model.News> GetNews();
        List<Model.News> GetAllNews();
    }
}
