using FollowService.Domain.Entities;
using FollowService.Infrastructure;
using FollowService.Infrastructure.Achieve;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FollowService.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FollowController : ControllerBase
    {
        private readonly NewFollow newFollow;

        public FollowController(NewFollow newFollow)
        {
            this.newFollow = newFollow;
        }

        [HttpPost]
        public Task<bool> LookFollows([FromBody]Follow follow)
        {
            return newFollow.LookFollow(follow);
        }

        [HttpPost]
        [UnitOfWork(typeof(MyDbContext))]
        public Task<bool> FollowAsyncs([FromBody] Follow follow)
        {
            return newFollow.FollowAsync(follow);
        }

        [HttpPost]
        [UnitOfWork(typeof(MyDbContext))]
        public Task<bool> RemoveFollows([FromBody] Follow follow)
        {
            return newFollow.RemoveFollow(follow);
        }

        [HttpPost]
        public List<Follow> GetAllFollows(int beingfollowed, int index)
        {
            return newFollow.GetAllFollow(beingfollowed, index);
        }

        [HttpPost]
        public object GetBeingfolloweds(int followers)
        {
            return newFollow.GetBeingfollowed(followers);
        }
    }
}
