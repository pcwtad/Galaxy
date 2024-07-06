using CommentService.Domain;
using CommentService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace commentService.Infrastructure.Achieve
{
    public class NewComment : IComment
    {
        private readonly MyDbContext myDbContext;

        public NewComment(MyDbContext myDbContext)
        {
            this.myDbContext = myDbContext;
        }

        public Task<bool> AddChildeveAsync(Childeve childeve)
        {
            myDbContext.Childeves.Add(childeve);
            return Task.FromResult(true);
        }

        public Task<bool> AddParenteveAsync(Parenteve parenteve)
        {
            myDbContext.Parenteve.Add(parenteve);
            return Task.FromResult(true);
        }

        public List<Childeve> GetChildeve(int parenteveId, int index)
        {
            int count = 10;
            List<Childeve> childevs = myDbContext.Childeves.Where(x => x.ParenteveId == parenteveId).OrderBy(x => x.CommentDate).Skip((index - 1) * count).Take(count).ToList();
            return childevs;
        }

        public List<Parenteve> GetParenteve(Guid showId, int index)
        {
            int count = 20;
            List<Parenteve> Parenteves= myDbContext.Parenteve.Where(x=>x.ShowId==showId).OrderBy(x => x.CommentDate).Skip((index - 1) * count).Take(count).ToList();
            return Parenteves;
        }
    }
}
