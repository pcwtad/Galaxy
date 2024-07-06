using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkService.Domain.Entities;

namespace WorkService.Domain
{
    public interface Ipreserve
    {
        //将作品数据保存
        Task<Guid> SaveworksAsync(Show show);
        //修改作品状态
        Task<bool> ModifystateAsync(long id, int showstate);
        //修改点赞数
        Task<bool> LikeworksAsync(long id, int showindex);
    }
}
