using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkService.Infrastructure;
using WorkService.Domain;
using WorkService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace WorkService.Infrastructure.Achieve
{
    public class FSRepository : IFSRepository
    {
        private readonly MyDbContext workDbContext;

        public FSRepository(MyDbContext workDbContext)
        {
            this.workDbContext = workDbContext;
        }

        public Task<DetailsImg?> FindImgAsync(long fileSize, string sha256Hash)
        {
            return workDbContext.DetailsImgs.FirstOrDefaultAsync(x=>x.FileSizeInBytes == fileSize&&x.FileSHA256Hash==sha256Hash);
        }
    }
}
