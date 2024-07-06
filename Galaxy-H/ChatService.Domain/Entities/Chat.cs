using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatService.Domain.Entities
{
    public class Chat
    {
        public int Id { get; init; }
        //消息发送人ID
        public string FormUserId { get; private set; }
        //消息接收人ID
        public string ToUserId { get; private set; }
        //消息内容
        public string Message { get; private set; }
        //消息发送时间
        public DateTime ChatTime { get; private set; }

        private Chat() { }

        public Chat(string formUserId, string toUserId, string message)
        {
            FormUserId = formUserId;
            ToUserId = toUserId;
            Message = message;
            ChatTime = DateTime.Now;
        }
    }
}
