using Microsoft.Extensions.DependencyInjection;
using NewMailKit;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using UserService.Domain;

namespace UserService.Infrastructure.Achieve
{
    
    public class Code : ICode
    {
        private readonly Mail mail;
        private readonly MyRedis myRedis;

        public Code(Mail mail, MyRedis myRedis)
        {
            this.mail = mail;
            this.myRedis = myRedis;
        }

        public async Task CodeAsync(string usermail)
        {

            ConnectionMultiplexer RedisDb = myRedis.ReturnIDatabase();
            IDatabase db = RedisDb.GetDatabase();

            //判断该值是否在数据库中
            if (db.KeyExists(usermail))
            {
                //在则删除
                db.KeyDeleteAsync(usermail);
            }
            //截取发件人邮箱地址从而判断Smtp的值
            string[] sArray = mail.fromPerson.Split(new char[2] { '@', '.' });
            if (sArray[1] == "qq")
            {
                mail.host = "smtp.qq.com";//如果是QQ邮箱则：smtp.qq.com,依次类推  163:smtp.163.com
            }
            else if (sArray[1] == "163")
            {
                mail.host = "smtp.163.com";
            }

            //将发件人邮箱带入MailAddress中初始化
            MailAddress mailAddress = new MailAddress(mail.fromPerson);
            //创建Email的Message对象


            using (MailMessage mailMessage = new MailMessage())
            {
                //添加收件人地址
                mailMessage.To.Add(usermail);
                //添加抄送地址
                mailMessage.To.Add(mail.mailCcArray);

                //发件人邮箱
                mailMessage.From = mailAddress;
                //标题
                mailMessage.Subject = mail.mailTitle;
                //编码
                mailMessage.SubjectEncoding = Encoding.UTF8;
                //生成四位验证码
                Random random = new Random();
                int randomNumber = random.Next(1000, 10000);
                //正文
                mailMessage.Body = "你好，你的验证码为：" + randomNumber.ToString();
                //正文编码
                mailMessage.BodyEncoding = Encoding.Default;
                //邮件优先级
                mailMessage.Priority = MailPriority.High;
                //正文是否是html格式
                mailMessage.IsBodyHtml = mail.isbodyHtml;

                //实例化一个Smtp客户端
                SmtpClient smtp = new SmtpClient();
                //将发件人的邮件地址和客户端授权码带入以验证发件人身份
                smtp.Credentials = new System.Net.NetworkCredential(mail.fromPerson, mail.code);
                //指定SMTP邮件服务器
                smtp.Host = mail.host;

                //邮件发送到SMTP服务器
                smtp.Send(mailMessage);

                //将值保存到数据库中，设置过期时间为10分钟
                db.StringSetAsync(usermail, randomNumber, TimeSpan.FromMinutes(10));
            }

            //关闭连接
            RedisDb.Close();
            RedisDb.Dispose();


        }
    }
}
