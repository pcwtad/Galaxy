using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommentService.Domain.Entities
{
    //子级评论
    public class Childeve
    {
        public int Id { get; init; }
        //最顶级的ID（对应Parenteve表中的Id）
        public int ParenteveId { get;private set; }
        //评论用户ID（对应User表中的ID）
        public int UserId { get; private set; }
        //被评论者ID
        public int CoverId { get; private set; }
        //评论内容
        public string Title { get; private set; }
        //评论时间
        public DateTime CommentDate { get; private set; }

        private Childeve() { }

        public Childeve(int parenteveId, int userId, int coverId, string title)
        {
            ParenteveId = parenteveId;
            UserId = userId;
            CoverId = coverId;
            Title = title;
            CommentDate = DateTime.Now;
        }
    }
}
