using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkService.Domain.Entities;

namespace WorkService.Domain
{
    public interface IDisplay
    {
        //将作品展示出来,分页查询
        List<Show> DisplayworksAsync(int index);
        //将指定用户作品展示出来,分页查询
        List<Show> DisplayworksListAsync(int index,int id);
    }
}
