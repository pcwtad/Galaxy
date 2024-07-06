using Microsoft.Extensions.Options;

namespace NewMailKit
{
    public class Mail
    {
        /// <summary>
        /// 发送人
        /// </summary>
        public string fromPerson = "2732916454@qq.com";

        /// <summary>
        /// 收件人地址
        /// </summary>
        public string recipientArry { get; set; }

        /// <summary>
        /// 抄送地址
        /// </summary>
        public string mailCcArray = "2732916454@qq.com";

        /// <summary>
        /// 标题
        /// </summary>
        public string mailTitle = "Galaxy";

        /// <summary>
        /// 正文
        /// </summary>
        public string? mailBody { get; set; }

        /// <summary>
        /// 客户端授权码(可存在配置文件中)
        /// </summary>
        public string code = "mwhpclkqlazrdfbg";

        /// <summary>
        /// SMTP邮件服务器
        /// </summary>
        public string? host { get; set; }

        /// <summary>
        /// 正文是否是html格式
        /// </summary>
        public bool isbodyHtml = true;
    }
}