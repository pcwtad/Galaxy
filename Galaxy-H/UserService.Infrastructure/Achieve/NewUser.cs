using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Domain;

namespace UserService.Infrastructure.Achieve
{
    public class NewUser : IUser
    {
        private readonly MyDbContext myDbContext;

        public NewUser(MyDbContext myDbContext)
        {
            this.myDbContext = myDbContext;
        }

        public Task<bool> ModifyUserintroduceAsync(int id, string userintroduce)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ModifyUserNameAsync(int id, string username)
        {
            throw new NotImplementedException();
        }

        public object ObtainUser(int id)
        {
            var userdomain = myDbContext.Users.Where(x => x.Id == id).Select(x => new
            {
                x.HeadImg,
                x.UserName,
                x.Userfwi,
                x.Userfans,
                x.Userlike,
                x.Userintroduce
            }).ToList();
            return userdomain;
        }
    }
}
