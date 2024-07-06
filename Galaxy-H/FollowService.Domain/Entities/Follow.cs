using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FollowService.Domain.Entities
{
    public class Follow
    {
        public int Id { get; init; }
        //关注者Id
        public int Followers { get; private set; }
        //被关注者Id
        public int Beingfollowed { get; private set; }
        //创建时间
        public DateTime FollowTime { get ; private set; }
        //关系0为关注，1为取关
        public int IsFollowing { get; set; }

        private Follow() { }

        public Follow(int followers, int beingfollowed)
        {
            Followers = followers;
            Beingfollowed = beingfollowed;
            FollowTime = DateTime.Now;
            IsFollowing = 0;
        }
    }
}
