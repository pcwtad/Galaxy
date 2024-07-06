using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Domain.Entities
{
    public class UserImgFile
    {
        public User user { get; init; }
        public int Id { get; set; }
        //头像图片大小(字节单位)
        public long? HeadImgSize { get;private set; }
        //头像路径
        public Uri? HeadImgUri { get; private set; }
        //头像SHA256
        public string? HeadImgSHA256 { get; private set; }
    }
}
