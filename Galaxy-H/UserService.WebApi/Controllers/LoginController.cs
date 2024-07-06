using Microsoft.AspNetCore.Mvc;
using UserService.Domain.Entities;
using UserService.Infrastructure;
using UserService.Infrastructure.Achieve;
using UserService.Infrastructure.other;
using UserService.WebApi.Commons;

namespace UserService.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly Code code;
        private readonly Login login;
        private readonly NewUser newUser;

        public LoginController(Code code, Login login, NewUser newUser)
        {
            this.code = code;
            this.login = login;
            this.newUser = newUser;
        }

        [HttpGet]
        public async Task SendCode(string mail)
        {
            //邮箱正则表达式
            ReturnRegex returnRegex = new ReturnRegex();
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            if (!returnRegex.FindRegex(mail, emailPattern)) return ;
            code.CodeAsync(mail);
        }

        [HttpPost]
        [UnitOfWork(typeof(MyDbContext))]
        public async Task<bool> UserRegis([FromBody] User user,string code)
        {
            //用户名正则表达式
            ReturnRegex returnRegex = new ReturnRegex();
            string namePattern = @"^.{2,8}$";
            if (!returnRegex.FindRegex(user.UserName, namePattern)) return false;
            bool isfalse = await login.UserRegisAsync(user,code);
            return isfalse;
        }

        [HttpPost]
        [UnitOfWork(typeof(MyDbContext))]
        public async Task<returnlogin?> PasswdLogin(string usermail, string userpasswod)
        {
            return await login.PasswdLoginAsync(usermail, userpasswod);
        }

        [HttpPost]
        [UnitOfWork(typeof(MyDbContext))]
        public async Task<returnlogin?> CodeLogin(string usermail, string code)
        {
            return await login.CodeLoginAsync(usermail, code);
        }

        [HttpPost]
        public object ObtainUsers(int id)
        {
            return newUser.ObtainUser(id);
        }

    }
}
