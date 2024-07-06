namespace ChatService.WebApi.other
{
    public class HubText
    {
        //发送人Id
        public string FormUserId { get; set; }
        //发送消息
        public string Message { get; set; }
        //接收者id
        public string ToUserId { get; set; }
    }
}
