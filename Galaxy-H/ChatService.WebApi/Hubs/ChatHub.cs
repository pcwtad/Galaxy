using ChatService.Domain.Entities;
using ChatService.Infrastructure;
using ChatService.WebApi.other;
using Microsoft.AspNetCore.SignalR;
using StackExchange.Redis;

namespace ChatService.WebApi.Hubs
{
    public class ChatHub : Hub
    {
        private readonly MyRedis myRedis;
        private readonly MyDbContext myDbContext;

        public ChatHub(MyRedis myRedis, MyDbContext myDbContext)
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
            string CtionID = "ChatID";
            db.HashSet(CtionID, Context.GetHttpContext().Request.Query["userId"].ToString(), Context.ConnectionId);
            await base.OnConnectedAsync();
        }

        // 当用户断开连接时，从映射中移除对应的项  
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            ConnectionMultiplexer RedisDb = myRedis.ReturnIDatabase();
            IDatabase db = RedisDb.GetDatabase();
            //定义哈希表名(将连接ID存储)
            string CtionID = "ChatID";
            db.HashDelete(CtionID, Context.GetHttpContext().Request.Query["userId"].ToString());
            await base.OnDisconnectedAsync(exception);
        }

        // 给指定用户发送聊天消息的方法  
        //[UnitOfWork(typeof(MyDbContext))]
        public async Task SendMsgToUser(string formUserId, string toUserId, string message)
        {
            HubText hubText = new HubText();
            hubText.FormUserId = formUserId;
            hubText.Message = message;
            hubText.ToUserId = toUserId;
            ConnectionMultiplexer RedisDb = myRedis.ReturnIDatabase();
            IDatabase db = RedisDb.GetDatabase();
            //定义哈希表名(将连接ID存储)
            string CtionID = "ChatID";
            //查找接收者信息
            var toconnection = db.HashGet(CtionID, toUserId);
            if (!toconnection.IsNull)//接收人在线向接收人发送消息
            {
                await Clients.Client(toconnection).SendAsync("ChatMessage", hubText);
            }
            await Clients.Client(Context.ConnectionId).SendAsync("ChatMessage", hubText);//给自己发一份

            //聊天记录保存数据库
            myDbContext.Chats.Add(new Chat(formUserId, toUserId, message));
            myDbContext.SaveChanges();
        }
    }
}
