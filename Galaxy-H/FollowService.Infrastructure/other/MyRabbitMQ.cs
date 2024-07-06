using MessagePack;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FollowService.Infrastructure.other
{
    public class MyRabbitMQ
    {
        public bool SendRabbitMQ(MyRabbitMQData myRabbitMQData)
        {

            var factory = new ConnectionFactory() { HostName = "localhost" };
            using var connection = factory.CreateConnection();
            using (var channel = connection.CreateModel())
            {
                // 声明一个直连交换机  
                channel.ExchangeDeclare(
                    exchange: "RabbitMQ_direct",//交换机名称
                    type: "direct",//交换机类型
                    durable: true
                    );

                // 声明一个队列  
                channel.QueueDeclare(
                    queue: "NewRabbitMQ",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                    );

                // 绑定队列到直连交换机，指定路由键  
                channel.QueueBind(
                    queue: "NewRabbitMQ",//要绑定的队列名称。
                    exchange: "RabbitMQ_direct",//要绑定的交换机名称。
                    routingKey: "direct.key"//是否将消息路由到这个队列
                    );
                var byteArrays = JsonConvert.SerializeObject(myRabbitMQData);
                var body = Encoding.UTF8.GetBytes(byteArrays);
                // 发送消息到直连交换机  
                channel.BasicPublish(
                    exchange: "RabbitMQ_direct",//交换机名称
                    routingKey: "direct.key",
                    basicProperties: null,
                    body: body
                    );
            }
            return true;
        }
    }
}
