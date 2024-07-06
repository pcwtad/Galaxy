using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatService.Domain
{
    public interface IChats
    {
        //查询聊天记录，分页
        object Chathistory(string fromuserid, string touserid, int index);
    }
}
