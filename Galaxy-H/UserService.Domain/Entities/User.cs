using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Domain.Entities
{
    public class User
    {
        public int Id { get; init; }
        //用户网名
        public string UserName { get; set; }
        //用户头像
        public string HeadImg { get; set; }
        //用户密码
        public string UserPasswod { get; set; }
        //用户关注数
        public int Userfwi { get; set; }
        //用户粉丝数
        public int Userfans { get; set; }
        //用户点赞数
        public int Userlike { get; set; }
        //用户创建时间
        public DateTime Usertime { get; init; }
        //用户介绍
        public string? Userintroduce { get; set; }
        //用户邮箱
        public string Usermail { get; set; }

        private User() { }

        public User(string userName, string userPasswod, string usermail)
        {
            UserName = userName;
            HeadImg = "https://sns-avatar-qc.xhscdn.com/avatar/645b80521e6a57c3bdb8f301.jpg?imageView2/2/w/540/format/webp";
            UserPasswod = userPasswod;
            Userfwi = 0;
            Userfans = 0;
            Userlike = 0;
            Usertime = DateTime.Now;
            Userintroduce = "我是" + userName;
            Usermail = usermail;
        }
    }
}
