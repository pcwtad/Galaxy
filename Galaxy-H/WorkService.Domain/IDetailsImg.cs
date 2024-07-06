using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkService.Domain.Entities;

namespace WorkService.Domain
{
    public interface IDetailsImg
    {
        //保存图片
        Task<bool> SavenimgAsync(DetailsImg detailsImg);
        //展示图片
        List<Uri> DisplayimgAsync(Guid id);
    }
}
