using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FollowService.Infrastructure
{
    public class MyRedis
    {
        public ConnectionMultiplexer ReturnIDatabase()
        {
            ConfigurationOptions configurationOptions = new ConfigurationOptions
            {
                EndPoints = { { "127.0.0.1", 6379 } }, // Redis服务器地址和端口  
            };
            ConnectionMultiplexer connection = ConnectionMultiplexer.Connect(configurationOptions);
            return connection;
        }
    }
}
