using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkService.Domain.other;

namespace WorkService.Domain
{
    public interface IStorageClient
    {
        //将图片保存返回保存路径以及SHA256
        Task<Imgother> SaveAsync(Stream content,string filename);
    }
}
