using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkService.Domain;
using WorkService.Domain.Entities;

namespace WorkService.Infrastructure.Achieve
{
    public class preserve : Ipreserve
    {
        private readonly MyDbContext workDbContext;

        public preserve(MyDbContext workDbContext)
        {
            this.workDbContext = workDbContext;
        }

        public Task<bool> LikeworksAsync(long id, int showindex)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ModifystateAsync(long id, int showstate)
        {
            throw new NotImplementedException();
        }

        public async Task<Guid> SaveworksAsync(Show show)
        {
            workDbContext.Shows.Add(show);
            return show.Id;
        }
    }
}
