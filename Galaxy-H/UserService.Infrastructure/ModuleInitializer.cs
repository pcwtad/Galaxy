using Microsoft.Extensions.DependencyInjection;
using NewMailKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zack.Commons;

namespace UserService.Infrastructure
{
    public class ModuleInitializer : IModuleInitializer
    {
        public void Initialize(IServiceCollection services)
        {
            services.AddScoped<MyRedis>();
            services.AddScoped<MyDbContext>();
            services.AddScoped<JWT>();
            services.AddScoped<Mail>();
        }
    }
}
