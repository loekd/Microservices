using CustomerService.Repositories;
using EventTypes;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PubSub;

namespace CustomerService
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
            //persisting stuff in memory, so we need singletons
            services.AddSingleton<ICustomerRepository, CustomerRepository>();

            var pubSubServiceHelper = new PubSubServiceHelper();
            services.AddSingleton<IPubSubServiceHelper>(pubSubServiceHelper);
            services.AddSingleton<IEventPublisher>(new PubSubServiceEventPublisher(pubSubServiceHelper));
            services.AddMvc()
                .AddXmlSerializerFormatters();

            services.AddApiVersioning(options =>
            {
                //backward compatibility:
                //options.AssumeDefaultVersionWhenUnspecified = true;
                //implicit v1:
                options.DefaultApiVersion = new ApiVersion(1, 0);
                //oldest version as default:
                //options.ApiVersionSelector = new LowestImplementedApiVersionSelector(options);
                //current version as default:
                //options.ApiVersionSelector = new CurrentImplementationApiVersionSelector(options);

                //mediatype: application/json;v=1.0
                //options.ApiVersionReader = new MediaTypeApiVersionReader();

                //custom header
                //options.ApiVersionReader = new HeaderApiVersionReader("x-version");

                //everything combined (don't do this)
                options.ApiVersionReader = ApiVersionReader.Combine(
                    new MediaTypeApiVersionReader(),
                    new HeaderApiVersionReader("x-version"), 
                    new QueryStringApiVersionReader(),
                    new UrlSegmentApiVersionReader());
            });
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
            var app = (IApplicationBuilder)state;
            var helper = app.ApplicationServices.GetRequiredService<IPubSubServiceHelper>();
            helper.RegisterWithPublisher("http://localhost:5000/api/subscription", typeof(OrderCreatedEvent)).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        private void UnregisterWithPublisher(object state)
        {
            //TODO: implement
        }
    }
}
