using FollowService.Domain;
using FollowService.Domain.Entities;
using FollowService.Infrastructure.other;
using MessagePack;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FollowService.Infrastructure.Achieve
{
    public class NewFollow : IFollow
    {
        private readonly MyDbContext myDbContext;
        private readonly MyRedis myRedis;
        private readonly MyRabbitMQ myRabbitMQ;

        public NewFollow(MyDbContext myDbContext, MyRedis myRedis, MyRabbitMQ myRabbitMQ)
        {
            this.myDbContext = myDbContext;
            this.myRedis = myRedis;
            this.myRabbitMQ = myRabbitMQ;
        }

        public async Task<bool> FollowAsync(Follow follow)
        {
            var isFollow = myDbContext.Follows.SingleOrDefault(x => x.Followers == follow.Followers && x.Beingfollowed == follow.Beingfollowed);
            if (isFollow!=null)
            {
                if (isFollow.IsFollowing == 0) return false;
                isFollow.IsFollowing = 0;

                //向Redis中添加对该用户的（Followers的）关注量与Beingfollowed的粉丝量，User项目中一段时间会进行同一添加与删除
                ConnectionMultiplexer RedisDb = myRedis.ReturnIDatabase();
                IDatabase db = RedisDb.GetDatabase();
                //定义关注量哈希键
                string fwikey = "myfwikey";
                //定义粉丝量哈希键
                string fanskey = "myfanskey";
                //Redis中对关注量哈希键操作
                if(db.HashExists(fwikey, follow.Followers))//查看是否有该字段
                {
                    //有则在原基础上加一
                    int fwikeyvalue = (int)db.HashGet(fwikey, follow.Followers);
                    int fwikeyvalues = fwikeyvalue+1;
                    db.HashSet(fwikey, follow.Followers, fwikeyvalues);
                }
                else
                {
                    int value = 1;
                    //没有则添加
                    db.HashSet(fwikey, follow.Followers, value);
                }

                //查看Redis中是否有粉丝量哈希键
                if (db.HashExists(fanskey, follow.Beingfollowed))//查看是否有该字段
                {
                    //有则在原基础上加一
                    int fanskeyvalue = (int)db.HashGet(fanskey, follow.Beingfollowed);
                    int fanskeyvalues = fanskeyvalue + 1;
                    db.HashSet(fanskey, follow.Beingfollowed, fanskeyvalues);
                }
                else
                {
                    int value = 1;
                    //没有则添加
                    db.HashSet(fanskey, follow.Beingfollowed, value);
                }

                //MyRabbitMQ myRabbitMQ=new MyRabbitMQ();
                MyRabbitMQData myRabbitMQData=new MyRabbitMQData();
                myRabbitMQData.Followers=follow.Followers;
                myRabbitMQData.Beingfollowed = follow.Beingfollowed;
                myRabbitMQ.SendRabbitMQ(myRabbitMQData);

                return true;
            }
            else
            {
                myDbContext.Follows.Add(follow);
                return true;
            }
        }

        public async Task<bool> LookFollow(Follow follow)
        {
            var isFollow = myDbContext.Follows.FirstOrDefault(x=>x.Followers== follow .Followers&& x.Beingfollowed== follow.Beingfollowed);
            if (isFollow == null)
            {
                return false;
            }
            if (isFollow.IsFollowing == 1) return false;
            return true;
        }

        public async Task<bool> RemoveFollow(Follow follow)
        {
            var isFollow = myDbContext.Follows.FirstOrDefault(x => x.Followers == follow.Followers && x.Beingfollowed == follow.Beingfollowed);
            if (isFollow == null)
            {
                return false;
            }
            isFollow.IsFollowing = 1;

            //向Redis中添加对该用户的（Followers的）关注量与Beingfollowed的粉丝量，User项目中一段时间会进行同一添加与删除
            ConnectionMultiplexer RedisDb = myRedis.ReturnIDatabase();
            IDatabase db = RedisDb.GetDatabase();
            //定义关注量哈希键
            string fwikey = "myfwikey";
            //定义粉丝量哈希键
            string fanskey = "myfanskey";
            //Redis中对关注量哈希键操作
            if (db.HashExists(fwikey, follow.Followers))//查看是否有该字段
            {
                //有则在原基础上加一
                int fwikeyvalue = (int)db.HashGet(fwikey, follow.Followers);
                int fwikeyvalues = fwikeyvalue - 1;
                db.HashSet(fwikey, follow.Followers, fwikeyvalues);
            }
            else
            {
                int index = -1;
                //没有则添加
                db.HashSet(fwikey, follow.Followers, index);
            }

            //查看Redis中是否有粉丝量哈希键
            if (db.HashExists(fanskey, follow.Beingfollowed))//查看是否有该字段
            {
                //有则在原基础上加一
                int fanskeyvalue = (int)db.HashGet(fanskey, follow.Beingfollowed);
                int fanskeyvalues = fanskeyvalue - 1;
                db.HashSet(fanskey, follow.Beingfollowed, fanskeyvalues);
            }
            else
            {
                int index = -1;
                //没有则添加
                db.HashSet(fanskey, follow.Beingfollowed, index);
            }

            return true;
        }


        public List<Follow> GetAllFollow(int beingfollowed,int index)
        {
            int count = 10;
            List<Follow> follows = myDbContext.Follows.Where(x => x.Beingfollowed == beingfollowed && x.IsFollowing == 0).Skip((index - 1) * count).Take(count).ToList();
            return follows;
        }

        public object GetBeingfollowed(int followers)
        {
            var Friend = myDbContext.Follows.Where(x => x.Followers == followers && x.IsFollowing == 0 && myDbContext.Follows.Any(y => y.Followers == x.Beingfollowed && y.Beingfollowed == followers && y.IsFollowing == 0)).Select(x => x.Beingfollowed).Distinct().ToList();
            return Friend;
        }
    }
}
