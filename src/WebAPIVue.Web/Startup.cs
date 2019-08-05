using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using ProxyKit;

namespace WebAPIVue.Web
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddProxy();

            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "app/dist";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.Map("/department", api =>
                api.RunProxy(context => context
                    .ForwardTo("http://localhost:62197")
                    .AddXForwardedHeaders()
                    .Send())
            );
            app.Map("/user", api =>
                api.RunProxy(context => context
                    .ForwardTo("http://localhost:62183")
                    .AddXForwardedHeaders()
                    .Send())
            );

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "app/dist";
            });
        }
    }
}
