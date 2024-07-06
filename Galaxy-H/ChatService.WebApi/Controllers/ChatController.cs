using ChatService.WebApi.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ChatService.WebApi.other;
using ChatService.Infrastructure;
using StackExchange.Redis;
using ChatService.Infrastructure.Achieve;

namespace ChatService.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IHubContext<Myhub> _hubContext;
        private readonly Myhub _myhub;
        private readonly MyRedis myRedis;
        private readonly Chats chats;


        public ChatController(IHubContext<Myhub> hubContext, Myhub myhub, MyRedis myRedis, Chats chats)
        {
            _hubContext = hubContext;
            _myhub = myhub;
            this.myRedis = myRedis;
            this.chats = chats;
        }

        [HttpPost]
        public async Task SendNotification([FromBody] MyRabbitMQData myRabbitMQData)
        {
            _myhub.BroadcastMessage(_hubContext, myRabbitMQData);
        }

        [HttpPost]
        public object SendChat(string fromuserid, string touserid, int index)
        {
            return chats.Chathistory(fromuserid, touserid, index);
        }
    }
}
