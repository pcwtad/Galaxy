using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkService.Domain;
using WorkService.Domain.Entities;

namespace WorkService.Infrastructure.Achieve
{
    public class Display : IDisplay
    {
        private readonly MyDbContext myDbContext;

        public Display(MyDbContext myDbContext)
        {
            this.myDbContext = myDbContext;
        }

        public List<Show> DisplayworksAsync(int index)
        {
            int count = 10;
            List<Show> ShowFind = myDbContext.Shows.OrderBy(x=>x.Id).Skip((index-1)*count).Take(count).ToList();
            return ShowFind;
        }

        public List<Show> DisplayworksListAsync(int index, int id)
        {
            int count = 10;
            List<Show> ShowFind = myDbContext.Shows.Where(x=>x.UserId==id.ToString()).OrderBy(x => x.Id).Skip((index - 1) * count).Take(count).ToList();
            return ShowFind;
        }
    }
}
