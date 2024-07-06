

using Follow.Other;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

var factory = new ConnectionFactory() { HostName = "localhost" };
using var connection = factory.CreateConnection();
using (var channel = connection.CreateModel())
{
    //接收消息  
    var consumer = new EventingBasicConsumer(channel);
    consumer.Received += (model, ea) =>
    {
        var body = ea.Body.ToArray();
        MyRabbitMQData myRabbitMQData = System.Text.Json.JsonSerializer.Deserialize<MyRabbitMQData>(body);
        HttpClient httpClient = new HttpClient();
        var jsonContent = JsonConvert.SerializeObject(myRabbitMQData);
        var stringContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
        httpClient.PostAsync("https://localhost:7189/api/Chat/SendNotification", stringContent);
        Console.WriteLine(1);
    };
    channel.BasicConsume(
        queue: "NewRabbitMQ",
        autoAck: true,
        consumer: consumer
        );

    Console.ReadLine();
}