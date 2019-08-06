using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.IO;
using WebAPIVue.DAL.CRUD;
using WebAPIVue.DAL.User;

namespace WebAPIVue.API.User
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(x => x.UseInMemoryDatabase("TestDbUser"));
            services.AddRouting();
            services.AddScoped<IDataManager<DAL.User.Entities.User>, DataManager<DAL.User.Entities.User, DataContext>>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouter(r =>
            {
                r.MapGet("", async (context) => {
                    using (var scope = app.ApplicationServices.CreateScope())
                    {
                        var repository = scope.ServiceProvider.GetService<IDataManager<DAL.User.Entities.User>>();
                        var users = await repository.GetAllAsync();
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(users, new JsonSerializerSettings
                        {
                            ContractResolver = new CamelCasePropertyNamesContractResolver()
                        }));
                    }
                });
                r.MapGet("{id:long}", async (request, response, routeData) => {
                    using (var scope = app.ApplicationServices.CreateScope())
                    {
                        var repository = scope.ServiceProvider.GetService<IDataManager<DAL.User.Entities.User>>();
                        var user = await repository.GetAsync(Convert.ToInt64(routeData.Values["id"]));
                        response.ContentType = "application/json";
                        await response.WriteAsync(JsonConvert.SerializeObject(user, new JsonSerializerSettings
                        {
                            ContractResolver = new CamelCasePropertyNamesContractResolver()
                        }));
                    }
                });
                r.MapPost("", async (context) => {
                    using (var scope = app.ApplicationServices.CreateScope())
                    {
                        var repository = scope.ServiceProvider.GetService<IDataManager<DAL.User.Entities.User>>();
                        using (var stream = new StreamReader(context.Request.Body))
                        {
                            var user = await repository.CreateAsync(JsonConvert.DeserializeObject<DAL.User.Entities.User>(stream.ReadToEnd()));
                            context.Response.StatusCode = 201;
                            context.Response.ContentType = "application/json";
                            await context.Response.WriteAsync(JsonConvert.SerializeObject(user, new JsonSerializerSettings
                            {
                                ContractResolver = new CamelCasePropertyNamesContractResolver()
                            }));
                        }
                    }
                });
                r.MapPut("", async (context) => {
                    using (var scope = app.ApplicationServices.CreateScope())
                    {
                        var repository = scope.ServiceProvider.GetService<IDataManager<DAL.User.Entities.User>>();
                        using (var stream = new StreamReader(context.Request.Body))                        
                        {
                            var user = await repository.CreateAsync(JsonConvert.DeserializeObject<DAL.User.Entities.User>(stream.ReadToEnd()));
                            context.Response.ContentType = "application/json";
                            await context.Response.WriteAsync(JsonConvert.SerializeObject(user, new JsonSerializerSettings
                            {
                                ContractResolver = new CamelCasePropertyNamesContractResolver()
                            }));
                        }
                    }
                });
                r.MapDelete("{id:long}", async (request, response, routeData) => {
                    using (var scope = app.ApplicationServices.CreateScope())
                    {
                        var repository = scope.ServiceProvider.GetService<IDataManager<DAL.User.Entities.User>>();
                        await repository.DeleteAsync(Convert.ToInt64(routeData.Values["id"]));
                        response.StatusCode = 204;
                    }
                });
            });
        }
    }
}
