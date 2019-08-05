using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using WebAPIVue.DAL.CRUD;
using WebAPIVue.DAL.Department;

namespace WebAPIVue.API.Department
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(x => x.UseInMemoryDatabase("TestDbDepartment"));
            services.AddRouting();
            services.AddScoped<IDataManager<DAL.Department.Entities.Department>, DataManager<DAL.Department.Entities.Department, DataContext>>();
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
                        var repository = scope.ServiceProvider.GetService<IDataManager<DAL.Department.Entities.Department>>();
                        var departments = await repository.GetAllAsync();
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(departments, new JsonSerializerSettings
                        {
                            ContractResolver = new CamelCasePropertyNamesContractResolver()
                        }));
                    }
                });
                r.MapGet("{id:long}", async (request, response, routeData) => {
                    using (var scope = app.ApplicationServices.CreateScope())
                    {
                        var repository = scope.ServiceProvider.GetService<IDataManager<DAL.Department.Entities.Department>>();
                        var department = await repository.GetAsync(Convert.ToInt64(routeData.Values["id"]));
                        response.ContentType = "application/json";
                        await response.WriteAsync(JsonConvert.SerializeObject(department, new JsonSerializerSettings
                        {
                            ContractResolver = new CamelCasePropertyNamesContractResolver()
                        }));
                    }
                });
                r.MapPost("", async (context) => {
                    using (var scope = app.ApplicationServices.CreateScope())
                    {
                        var repository = scope.ServiceProvider.GetService<IDataManager<DAL.Department.Entities.Department>>();
                        using (var stream = new StreamReader(context.Request.Body))
                        {
                            var department = await repository.CreateAsync(JsonConvert.DeserializeObject<DAL.Department.Entities.Department>(stream.ReadToEnd()));
                            context.Response.StatusCode = 201;
                            context.Response.ContentType = "application/json";
                            await context.Response.WriteAsync(JsonConvert.SerializeObject(department, new JsonSerializerSettings
                            {
                                ContractResolver = new CamelCasePropertyNamesContractResolver()
                            }));
                        }
                    }
                });
                r.MapPut("", async (context) => {
                    using (var scope = app.ApplicationServices.CreateScope())
                    {
                        var repository = scope.ServiceProvider.GetService<IDataManager<DAL.Department.Entities.Department>>();
                        using (var stream = new StreamReader(context.Request.Body))
                        {
                            var department = await repository.CreateAsync(JsonConvert.DeserializeObject<DAL.Department.Entities.Department>(stream.ReadToEnd()));
                            context.Response.ContentType = "application/json";
                            await context.Response.WriteAsync(JsonConvert.SerializeObject(department, new JsonSerializerSettings
                            {
                                ContractResolver = new CamelCasePropertyNamesContractResolver()
                            }));
                        }
                    }
                });
                r.MapDelete("{id:long}", async (request, response, routeData) => {
                    using (var scope = app.ApplicationServices.CreateScope())
                    {
                        var repository = scope.ServiceProvider.GetService<IDataManager<DAL.Department.Entities.Department>>();
                        await repository.DeleteAsync(Convert.ToInt64(routeData.Values["id"]));
                        response.StatusCode = 204;
                    }
                });
            });
        }
    }
}
