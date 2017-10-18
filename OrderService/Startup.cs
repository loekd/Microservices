using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
            services.AddSingleton<IPubSubServiceHelper>(new PubSubServiceHelper());
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
            helper.RegisterWithPublisher("http://localhost:2000/api/subscribe", "OrderCreated").ConfigureAwait(false).GetAwaiter().GetResult();
        }

        private void UnregisterWithPublisher(object state)
        {
            //TODO: implement
        }
    }
}
