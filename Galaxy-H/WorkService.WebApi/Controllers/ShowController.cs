using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkService.Domain.Entities;
using WorkService.Infrastructure;
using WorkService.Infrastructure.Achieve;

namespace WorkService.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ShowController : ControllerBase
    {
        private readonly NewDetailsImg newDetailsImg;
        private readonly preserve preserve;
        private readonly StorageClient storageClient;

        public ShowController(NewDetailsImg newDetailsImg, preserve preserve, StorageClient storageClient)
        {
            this.newDetailsImg = newDetailsImg;
            this.preserve = preserve;
            this.storageClient = storageClient;
        }

        [HttpPost]
        [UnitOfWork(typeof(MyDbContext))]
        public async Task<object> Showincrease([FromForm] OeEntity oeEntity,string userId,string showbrin,string showwhere,string showaddress,string showinformation)
        {
            int count = 0;
            Guid Id=Guid.NewGuid();
            foreach (var v in oeEntity.Files)
            {
                var ShaUri = await storageClient.SaveAsync(v.OpenReadStream(), v.FileName);
                if (count == 0)
                {
                    Id = await preserve.SaveworksAsync(new Show(ShaUri.RemoteUrl, userId, showbrin, showwhere, showaddress, showinformation));
                }
                newDetailsImg.SavenimgAsync(new DetailsImg(Id, v.Length, ShaUri.FileSHA256Hash, ShaUri.RemoteUrl));
                count++;
            }
            return count;
        }

    }
}
