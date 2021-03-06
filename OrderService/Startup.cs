﻿using EventTypes;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PubSub;

namespace OrderService
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
            var pubSubServiceHelper = new PubSubServiceHelper();
            services.AddSingleton<IPubSubServiceHelper>(pubSubServiceHelper);
            services.AddSingleton<IEventPublisher>(new PubSubServiceEventPublisher(pubSubServiceHelper));
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime applicationLifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            applicationLifetime.ApplicationStarted.Register(RegisterWithPublisher, app);
            applicationLifetime.ApplicationStopping.Register(UnregisterWithPublisher, app);
        }

        private void RegisterWithPublisher(object state)
        {
            var app = (IApplicationBuilder) state;
            var helper = app.ApplicationServices.GetRequiredService<IPubSubServiceHelper>();
            helper.RegisterWithPublisher("http://localhost:2000/api/subscription", typeof(CustomerCreatedEvent)).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        private void UnregisterWithPublisher(object state)
        {
            //TODO: implement
        }
    }
}
