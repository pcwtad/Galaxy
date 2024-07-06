using ChatService.Infrastructure;
using ChatService.WebApi.other;
using Microsoft.AspNetCore.SignalR;
//using Microsoft.AspNetCore.SignalR;
using StackExchange.Redis;

namespace ChatService.WebApi.Hubs
{
    public class Myhub : Hub
    {
        private readonly MyRedis myRedis;
        private readonly MyDbContext myDbContext;

        public Myhub(MyRedis myRedis, MyDbContext myDbContext)
        {
            this.myRedis = myRedis;
            this.myDbContext = myDbContext;
        }

        // 当用户连接时，将其用户ID和连接ID存储到映射中  
        public override async Task OnConnectedAsync()
        {
            ConnectionMultiplexer RedisDb = myRedis.ReturnIDatabase();
            IDatabase db = RedisDb.GetDatabase();
            //定义哈希表名(将连接ID存储)
            string CtionID = "CtionID";
            db.HashSet(CtionID, Context.GetHttpContext().Request.Query["userId"].ToString(), Context.ConnectionId);
            await base.OnConnectedAsync();
        }

        // 当用户断开连接时，从映射中移除对应的项  
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            ConnectionMultiplexer RedisDb = myRedis.ReturnIDatabase();
            IDatabase db = RedisDb.GetDatabase();
            //定义哈希表名(将连接ID存储)
            string CtionID = "CtionID";
            db.HashDelete(CtionID, Context.GetHttpContext().Request.Query["userId"].ToString());
            await base.OnDisconnectedAsync(exception);
        }

        // 给指定用户发送聊天消息的方法  
        public async Task SendMsgToUser(string formUserId, string toUserId, string message)
        {
            HubText hubText = new HubText();
            hubText.FormUserId = formUserId;
            hubText.Message = message;
            ConnectionMultiplexer RedisDb = myRedis.ReturnIDatabase();
            IDatabase db = RedisDb.GetDatabase();
            //定义哈希表名(将连接ID存储)
            string CtionID = "CtionID";
            //查找接收者信息
            var toconnection = db.HashGet(CtionID, toUserId);
            if (!toconnection.IsNull)//接收人在线向接收人发送消息
            {
                await Clients.Client(toconnection).SendAsync("ReceiveMessage", hubText);
            }

            //聊天记录保存数据库
        }

        //给指定用户发送被关注消息
        public async Task BroadcastMessage(IHubContext<Myhub> hubContext,MyRabbitMQData myRabbitMQData)
        {
            ConnectionMultiplexer RedisDb = myRedis.ReturnIDatabase();
            IDatabase db = RedisDb.GetDatabase();
            //定义哈希表名(将连接ID存储)
            string CtionID = "CtionID";

            string connectionId = db.HashGet(CtionID, myRabbitMQData.Beingfollowed);
            await hubContext.Clients.Client(connectionId).SendAsync("ReceiveMessage", myRabbitMQData);//给接收人发一份
        }
    }
}
