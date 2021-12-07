using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xend.AppService;
using Xend.AppService.Helpers;
using Xend.AppService.MemoryService;
using Xend.AppService.RabbitMQHelpers;

namespace Xend.TransactionServiceHandler
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
            services.AddMemoryCache();
            services.AddControllers();
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddSingleton<IMemoryCacheService, MemoryCacheService>();
            services.AddTransient<ITransactionProcessor, TransactionProcessor>();
            services.AddTransient<ITransactionService, TransactionService>();
            services.AddMassTransit(x =>
            {
          
                x.AddConsumer<TransactionUpdateConsumer>();
                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    cfg.Host("rabbitmq://guest:guest@localhost:5672");
                    cfg.ReceiveEndpoint("tnxQueue", ep =>
                    {
                        //ep.PrefetchCount = 16;
                        //ep.UseMessageRetry(r=> r.Interval(1, TimeSpan.FromSeconds(60))); // version conflict
                        ep.ConfigureConsumer<TransactionUpdateConsumer>(provider);
                    });
                }));
            });


            //var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
            //{
            //    cfg.Host("amqp://guest:guest@localhost:15672");
            //    cfg.ReceiveEndpoint("todoQueue", ep =>
            //    {
            //        ep.PrefetchCount = 16;
            //        //ep.UseMessageRetry(r => r.Interval(2, 100));//package conflict.
            //        ep.Consumer<TransactionUpdateConsumer>();
            //    });

            //});
            services.AddMassTransitHostedService();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Xend.TransactionServiceHandler", Version = "v1" });
            });

         
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }               
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Xend.TransactionServiceHandler v1"));
            app.UseSwagger();

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
