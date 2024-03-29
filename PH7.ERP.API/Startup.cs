using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using PH7.ERP.BLL;
using PH7.ERP.DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PH7.ERP.API
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
            //注册
            services.AddTransient<SqlServerHelper>();
            services.AddTransient<Hospital_BLL>();
            services.AddTransient<Department_BLL>();
            services.AddTransient<Doctor_detailed_BLL>();
            services.AddTransient<Doctor_money_BLL>();
            services.AddTransient<Doctor_relation_BLL>();
            services.AddTransient<DoctorLog_BLL>();
            services.AddTransient<Grade_BLL>();
            services.AddTransient<Patient_BLL>();
            services.AddTransient<Disease_records_BLL>();
            services.AddTransient<Live_TV_BLL>();



            //1.配置跨域处理，允许所有来源： 
            services.AddCors(options =>
                options.AddPolicy("自定义的跨域策略名称", p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader())
            );


            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PH7.ERP.API", Version = "v1" });



                // 为 Swagger 设置xml文档注释路径
                var basePath = AppContext.BaseDirectory;
                //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //当前接口目录下生成该文件 bin\Debug\netcoreapp3.1\MyDemo.WebApi.xml

                var xmlPath = Path.Combine(basePath, "PH7.ERP.API.xml");
                if (File.Exists(xmlPath))
                {
                    c.IncludeXmlComments(xmlPath, true);
                }

            });


        }




        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PH7.ERP.API v1"));
            }




            app.UseCors("自定义的跨域策略名称");//必须位于UserMvc之前 
            app.UseStaticFiles();               //允许静态图片
            app.UseRouting();
            //



            app.UseAuthorization();

            //路由设置 默认
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

}