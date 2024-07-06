using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkService.Infrastructure.Achieve;

namespace WorkService.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DetaController : ControllerBase
    {
        private readonly NewDetailsImg newDetailsImg;

        public DetaController(NewDetailsImg newDetailsImg)
        {
            this.newDetailsImg = newDetailsImg;
        }

        [HttpPost]
        public List<Uri> oteurls(Guid id)
        {
            return newDetailsImg.DisplayimgAsync(id);
        }
    }
}
