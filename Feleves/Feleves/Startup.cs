using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Logic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Models;
using Repository;

namespace Feleves
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<VelemenyContextDb>();
            services.AddTransient<VelemenyLogic, VelemenyLogic>();
            services.AddTransient<KesLogic, KesLogic>();
            services.AddTransient<KesBoltLogic, KesBoltLogic>();
            services.AddTransient<NemCRUDLogic, NemCRUDLogic>();
            services.AddTransient<IRepository<Velemeny>, VelemenyRepo>();
            services.AddTransient<IRepository<Kes>, KesRepo>();
            services.AddTransient<IRepository<Kes_Bolt>, KesBoltRepo>();
            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "KnifeStores Api endpoints", Version = "v1" });
            });
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                                  builder =>
                                  {
                                      builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                                  });
            });
            services.AddMvc(opt => opt.EnableEndpointRouting = false);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors();
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger();
            app.UseSwaggerUI(opt =>
            {
                opt.SwaggerEndpoint("./v1/swagger.json", "KnifeStore Api");
            });
        }
    }
}
