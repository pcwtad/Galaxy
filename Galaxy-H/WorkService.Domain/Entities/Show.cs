using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkService.Domain.Entities
{
    public class Show
    {
        public Guid Id { get; init; }
        //发布人的用户ID,是用户表中的Id
        public string UserId { get; private set; }
        //作品介绍
        public string Showbrin { get; private set; }
        //作品封面图
        public Uri Showcover { get; set; }
        //作品发布时间
        public DateTime ShowDatetime { get; init; }
        //作品地址省级
        public string Showwhere { get; private set; }
        //作品地址全地址
        public string Showaddress { get; private set; }
        //作品点赞数
        public int Showindex { get; set; }
        //作品详情信息
        public string Showinformation { get; private set; }
        //作品状态，0审核中，1审核成功，2审核失败，3已删除
        public int Showstate { get; set; }

        public Show(Uri showcover, string userId, string showbrin, string showwhere, string showaddress, string showinformation)
        {
            Id = Guid.NewGuid();
            Showcover = showcover;
            UserId = userId;
            Showbrin = showbrin;
            ShowDatetime = DateTime.Now;
            Showwhere = showwhere;
            Showaddress = showaddress;
            Showindex = 0;
            Showinformation = showinformation;
            Showstate = 0;
        }
    }
}
