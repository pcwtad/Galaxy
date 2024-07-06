using FollowService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FollowService.Domain
{
    public interface IFollow
    {
        //关注此人（true成功）
        Task<bool> FollowAsync(Follow follow);
        //查询是否关注此人（false没有）
        Task<bool> LookFollow(Follow follow);
        //取关（true成功）
        Task<bool> RemoveFollow(Follow follow);
        //查询所有关注我的人
        List<Follow> GetAllFollow(int beingfollowed, int index);
        //查询所有我关注的人
        object GetBeingfollowed(int followers);
    }
}
