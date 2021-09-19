using MarsRoverAPI.FileRepository;
using MarsRoverAPI.Redis;
using MarsRoverProject.Contracts;
using MarsRoverProject.Contracts.Persistance;
using MarsRoverProject.Managers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using StackExchange.Redis.Extensions.Core;
using StackExchange.Redis.Extensions.Core.Configuration;
using StackExchange.Redis.Extensions.Newtonsoft;

namespace MarsRoverAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MarsRoverAPI", Version = "v1" });
            });

            var marsSurfaceRepoConf = Configuration.GetSection("MarsSurfaceRepository");
            if (marsSurfaceRepoConf.GetSection("UseFile").Get<bool>())
            {
                services.AddScoped<IMarsSurfaceRepository, FileMarsSurfaceRepository>();
            }
            else
            {
                services.Configure<AppRedisConfiguration>(Configuration.GetSection(nameof(AppRedisConfiguration)));

                services.AddScoped<IMarsSurfaceRepository, RedisMarsSurfaceRepository>();

                var redisConfiguration = Configuration.GetSection("Redis").Get<RedisConfiguration>();

                services.AddSingleton<ISerializer, NewtonsoftSerializer>();
                services.AddStackExchangeRedisExtensions<NewtonsoftSerializer>(redisConfiguration);
            }

            services.AddScoped<ICommandReciever, CommandReciever>();
            services.AddScoped<IRoverManager, RoverManager>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MarsRoverAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
