using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class NewsRepository : Interfaces.INews
    {
        DataAccess.ShopAnnaContainer _context = new DataAccess.ShopAnnaContainer();

        public List<News> GetAllNews()
        {
            return _context.News.OrderByDescending(x => x.Id).ToList();
        }

        public List<News> GetNews()
        {
            return _context.News.OrderByDescending(x=> x.Id).Take(2).ToList();
        }
    }
}
