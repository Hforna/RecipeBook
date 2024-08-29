using Microsoft.Extensions.DependencyInjection;
using ProjectAspNet.Application.Services.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Application
{
    public static class DependencyInjectionApplication
    {
        public static void AddApplication(this IServiceCollection services)
        {
            AddMapper(services);
        }

        public static void AddMapper(IServiceCollection services)
        {
            var mapper = new AutoMapper.MapperConfiguration(x => { x.AddProfile(new UserMappper()); }).CreateMapper();
            services.AddScoped(opt => mapper);
        }
    }
}
