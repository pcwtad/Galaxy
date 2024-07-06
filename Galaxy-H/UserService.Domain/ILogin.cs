using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Domain.Entities;
using UserService.Infrastructure.other;

namespace UserService.Domain
{
    public interface ILogin
    {
        //根据邮箱号密码登录
        Task<returnlogin?> PasswdLoginAsync(string usermail,string userpasswod);
        //根据邮箱验证码登录
        Task<returnlogin?> CodeLoginAsync(string usermail, string code);
        //注册账号
        Task<bool> UserRegisAsync(User user,string code);
        //修改邮箱号
        Task<bool> ModifyEmailAsync(int guid, string oldmail, string oldcode);
        //修改密码
        Task<bool> ModifyPasswdAsync(int guid, string passwd);
        //判断验证码是否正确
        Task<bool> IsEmailCodeAsync(string email,string code);
        //判断邮箱号是否已经注册(同时判断是否有该用户)
        Task<bool> IsEmailAsync(string email);
    }
}
