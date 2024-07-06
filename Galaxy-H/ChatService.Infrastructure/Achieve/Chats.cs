using ChatService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatService.Infrastructure.Achieve
{
    public class Chats : IChats
    {
        private readonly MyDbContext myDbContext;

        public Chats(MyDbContext myDbContext)
        {
            this.myDbContext = myDbContext;
        }

        public object Chathistory(string fromuserid, string touserid, int index)
        {
            var chatry = myDbContext.Chats.Where(x=>x.FormUserId==fromuserid&&x.ToUserId==touserid||x.ToUserId==fromuserid&&x.FormUserId==touserid).OrderByDescending(x=>x.ChatTime).Skip((index - 1) * 20).Take(20).OrderBy(x => x.ChatTime).Select(x => new
            {
                x.FormUserId,
                x.ToUserId,
                x.Message
            }).ToList();
            return chatry;
        }
    }
}
