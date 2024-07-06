using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WorkService.Domain;
using WorkService.Domain.other;

namespace WorkService.Infrastructure.Achieve
{
    public class StorageClient : IStorageClient
    {
        public async Task<Imgother> SaveAsync(Stream content,string filename)
        {
            //保存图片
            var FilesPath = $"E:/项目/Wei-Galaxy/ShowImg/{DateTime.Now:yyyyMMdd}";
            if (!Directory.Exists(FilesPath))
            {
                Directory.CreateDirectory(FilesPath);
            }
            var FileimgPath=Path.Combine(FilesPath, filename);
            if (File.Exists(FileimgPath))//如果已经存在，则尝试删除
            {
                File.Delete(FileimgPath);
            }
            using Stream outStream = File.OpenWrite(FileimgPath);
            await content.CopyToAsync(outStream);
            //获取SHA256
            string hex = "";
            try
            {
                byte[] hash;
                using (content)
                {
                    using (SHA256 sha256 = SHA256.Create())
                    {
                        hash = sha256.ComputeHash(content);
                    }
                }
                // 将字节数组转换为十六进制字符串  
                hex = BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            Imgother imgother = new Imgother();
            imgother.FileSHA256Hash=hex;
            imgother.RemoteUrl = new Uri(FileimgPath);
            return imgother;
        }
    }
}

