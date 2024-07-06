using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkService.Domain.Entities
{
    public class DetailsImg
    {
        public long Id { get; set; }
        //对应Show表中的ID
        public Guid ShowId { get; private set; }
        //文件大小（尺寸为字节）
        public long FileSizeInBytes { get; private set; }
        //文件散列值（SHA256）
        public string FileSHA256Hash { get; private set; }
        //上传的文件的供外部访问者访问的路径
        public Uri RemoteUrl { get; private set; }

        public DetailsImg(Guid showId, long fileSizeInBytes, string fileSHA256Hash, Uri remoteUrl)
        {
            ShowId = showId;
            FileSizeInBytes = fileSizeInBytes;
            FileSHA256Hash = fileSHA256Hash;
            RemoteUrl = remoteUrl;
        }
    }
}
