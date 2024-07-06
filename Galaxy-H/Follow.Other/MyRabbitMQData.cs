using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Follow.Other
{
    public class MyRabbitMQData
    {
        //关注者
        public int Followers { get; set; }
        //被关注者
        public int Beingfollowed { get; set; }
        //关注
        public string Follow { get; set; }
    }
}
