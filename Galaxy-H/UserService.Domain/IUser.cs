using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Domain
{
    public interface IUser
    {
        //修改用户网名
        Task<bool> ModifyUserNameAsync(int id, string username);
        //修改介绍
        Task<bool> ModifyUserintroduceAsync(int id, string userintroduce);
        //获取用户
        object ObtainUser(int id);
    }
}
