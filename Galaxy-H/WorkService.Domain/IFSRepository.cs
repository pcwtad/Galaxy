using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkService.Domain.Entities;

namespace WorkService.Domain
{
    public interface IFSRepository
    {
        //根据散列值和文件大小查找是否有相同图片
        Task<DetailsImg?> FindImgAsync(long fileSize, string sha256Hash);
    }
}
