using Quartz;
using StackExchange.Redis;
using UserService.Infrastructure;

namespace UserService.WebApi.Quartzs
{
    public class MyJob : IJob
    {
        private readonly MyRedis myRedis;
        private readonly MyDbContext myDbContext;

        public MyJob(MyRedis myRedis, MyDbContext myDbContext)
        {
            this.myRedis = myRedis;
            this.myDbContext = myDbContext;
        }

        //执行SqlServer和Redis数据一致（Follow服务中的关注与粉丝数量一致操作）
        public Task Execute(IJobExecutionContext context)
        {
            //定义关注量哈希键
            string fwikey = "myfwikey";
            //定义粉丝量哈希键
            string fanskey = "myfanskey";
            ConnectionMultiplexer RedisDb = myRedis.ReturnIDatabase();
            IDatabase db = RedisDb.GetDatabase();
            //统一添加关注量
            foreach (var item in db.HashGetAll(fwikey))
            {
                var Userkey = myDbContext.Users.FirstOrDefault(x => x.Id == (int)item.Name);
                Userkey.Userfwi = (int)item.Value+ Userkey.Userfwi;
                db.HashDelete(fwikey, item.Name);
            }
            //统一添加粉丝数
            foreach (var item in db.HashGetAll(fanskey))
            {
                var Userkey = myDbContext.Users.FirstOrDefault(x => x.Id == (int)item.Name);
                Userkey.Userfans = (int)item.Value+ Userkey.Userfans;
                db.HashDelete(fanskey, item.Name);
            }
            myDbContext.SaveChanges();
            return Task.CompletedTask;
        }
    }
}
