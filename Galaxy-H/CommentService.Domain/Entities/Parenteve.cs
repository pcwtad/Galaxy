using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommentService.Domain.Entities
{
    //顶级评论
    public class Parenteve
    {
        public int Id { get; init; }
        //评论作品ID(对应Show表中的ID)
        public Guid ShowId { get; private set; }
        //评论用户ID（对应User表中的ID）
        public int UserId { get; private set; }
        //评论内容
        public string Title { get; private set; }
        //评论时间
        public DateTime CommentDate { get; private set; }

        private Parenteve() { }

        public Parenteve(Guid showId, int userId, string title)
        {
            ShowId = showId;
            UserId = userId;
            Title = title;
            CommentDate = DateTime.Now;
        }
    }
}
