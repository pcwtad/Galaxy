using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkService.Domain;
using WorkService.Domain.Entities;

namespace WorkService.Infrastructure.Achieve
{
    public class NewDetailsImg : IDetailsImg
    {
        private readonly MyDbContext workDbContext;

        public NewDetailsImg(MyDbContext workDbContext)
        {
            this.workDbContext = workDbContext;
        }

        public List<Uri> DisplayimgAsync(Guid id)
        {
            List<Uri> oteurl = workDbContext.DetailsImgs.Where(x => x.ShowId == id).Select(x => x.RemoteUrl).ToList();
            return oteurl;
        }

        public async Task<bool> SavenimgAsync(DetailsImg detailsImg)
        {
            workDbContext.DetailsImgs.Add(detailsImg);
            return true;
        }
    }
}
