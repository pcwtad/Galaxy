using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Domain;
using UserService.Domain.Entities;
using UserService.Infrastructure.other;

namespace UserService.Infrastructure.Achieve
{
    public class Login : ILogin
    {
        private MyRedis myRedis;
        private MyDbContext myDbContext;
        private JWT jWT;

        public Login(MyRedis myRedis, MyDbContext myDbContext, JWT jWT)
        {
            this.myRedis = myRedis;
            this.myDbContext = myDbContext;
            this.jWT = jWT;
        }

        //根据邮箱找用户
        public async Task<User?> LookmailUser(string usermail)
        {
            User user = myDbContext.Users.FirstOrDefault(x=>x.Usermail == usermail);
            return user;
        }
        
        //根据ID找用户
        public async Task<User?> LookidUser(int id)
        {
            User user=myDbContext.Users.SingleOrDefault(x=>x.Id == id);
            return user;
        }

        public async Task<returnlogin?> CodeLoginAsync(string usermail, string code)
        {
            returnlogin returnlogins = new returnlogin();
            if (!await IsEmailAsync(usermail)) return null;
            if(!await IsEmailCodeAsync(usermail,code)) return null;
            User user = await LookmailUser(usermail);
            returnlogins.jwt= jWT.GetJwt(usermail);
            returnlogins.Id = user.Id;
            return returnlogins;

        }

        public async Task<bool> IsEmailAsync(string email)
        {
            if (LookmailUser(email) == null)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> IsEmailCodeAsync(string email, string code)
        {
            ConnectionMultiplexer RedisDb = myRedis.ReturnIDatabase();
            IDatabase db = RedisDb.GetDatabase();
            if (code != await db.StringGetAsync(email))
            {
                return false;
            }
            db.KeyDeleteAsync(email);
            return true;
        }

        //false表失败
        public async Task<bool> ModifyEmailAsync(int guid, string oldmail, string oldcode)
        {
            if(!await IsEmailAsync(oldmail)) return false;
            if(!await IsEmailCodeAsync(oldmail,oldcode)) return false;
            User user = await LookidUser(guid);
            if(user==null) return false;
            user.Usermail=oldmail;
            return true;
        }

        public async Task<bool> ModifyPasswdAsync(int guid, string passwd)
        {
            User user = await LookidUser(guid);
            if (user == null) return false;
            user.UserPasswod = passwd;
            return true;
        }

        public async Task<returnlogin?> PasswdLoginAsync(string usermail, string userpasswod)
        {
            returnlogin returnlogins = new returnlogin();
            User user = await LookmailUser(usermail);
            if (user == null) return null;
            if(user.UserPasswod!=userpasswod) return null;
            returnlogins.jwt = jWT.GetJwt(usermail);
            returnlogins.Id=user.Id;
            return returnlogins;
        }

        public async Task<bool> UserRegisAsync(User user, string code)
        {
            if (await LookmailUser(user.Usermail)!=null) return false;
            if(!await IsEmailCodeAsync(user.Usermail,code)) return false;
            myDbContext.Users.Add(user);
            return true;
        }
    }
}
