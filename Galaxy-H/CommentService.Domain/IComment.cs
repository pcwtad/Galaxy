using CommentService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommentService.Domain
{
    public interface IComment
    {
        //根据作品获取顶级评论(每次获取20行数据)
        List<Parenteve> GetParenteve(Guid showId, int index);
        //根据顶级ID获取回复评论（每次获取10行数据）
        List<Childeve> GetChildeve(int parenteveId, int index);
        //添加顶级评论
        Task<bool> AddParenteveAsync(Parenteve parenteve);
        //添加二级评论
        Task<bool> AddChildeveAsync(Childeve childeve);
    }
}
