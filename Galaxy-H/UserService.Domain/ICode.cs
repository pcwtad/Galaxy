using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Domain
{
    public interface ICode
    {
        //邮箱发送验证码
        Task CodeAsync(string usermail);
    }
}
