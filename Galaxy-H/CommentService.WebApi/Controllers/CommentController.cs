using commentService.Infrastructure;
using commentService.Infrastructure.Achieve;
using CommentService.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CommentService.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly NewComment newComment;

        public CommentController(NewComment newComment)
        {
            this.newComment = newComment;
        }

        [HttpPost]
        [UnitOfWork(typeof(MyDbContext))]
        public async Task<bool> AddChildeveAsyncs([FromBody] Childeve childeve)
        {
            return await newComment.AddChildeveAsync(childeve);
        }

        [HttpPost]
        [UnitOfWork(typeof(MyDbContext))]
        public async Task<bool> AddParenteveAsyncs([FromBody] Parenteve parenteve)
        {
            return await newComment.AddParenteveAsync(parenteve);
        }

        [HttpPost]
        public List<Childeve> GetChildeves(int parenteveId, int index)
        {
            return newComment.GetChildeve(parenteveId, index);
        }

        [HttpPost]
        public List<Parenteve> GetParenteves(Guid showId, int index)
        {
            return newComment.GetParenteve(showId, index);
        }
    }
}
