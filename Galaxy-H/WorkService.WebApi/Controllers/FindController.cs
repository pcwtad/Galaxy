using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkService.Domain.Entities;
using WorkService.Infrastructure.Achieve;

namespace WorkService.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FindController : ControllerBase
    {
        private readonly Display display;

        public FindController(Display display)
        {
            this.display = display;
        }

        [HttpPost]
        public List<Show> DisplayFind(int x)
        {
            return display.DisplayworksAsync(x);
        }

        [HttpPost]
        public List<Show> DisplayMy(int x,int id)
        {
            return display.DisplayworksListAsync(x, id);
        }
    }
}
