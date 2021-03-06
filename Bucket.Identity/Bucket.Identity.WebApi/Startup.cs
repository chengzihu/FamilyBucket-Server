﻿using Bucket.AspNetCore.Extensions;
using Bucket.AspNetCore.Filters;
using Bucket.Authorize.Extensions;
using Bucket.Caching.Extensions;
using Bucket.Caching.StackExchangeRedis;
using Bucket.Config.Extensions;
using Bucket.Config.HostedService;
using Bucket.DbContext;
using Bucket.DependencyInjection;
using Bucket.ErrorCode.Extensions;
using Bucket.ErrorCode.HostedService;
using Bucket.EventBus.Extensions;
using Bucket.EventBus.RabbitMQ.Extensions;
using Bucket.HostedService.AspNetCore;
using Bucket.LoadBalancer.Extensions;
using Bucket.ServiceDiscovery.Consul.Extensions;
using Bucket.ServiceDiscovery.Extensions;
using Bucket.SkyApm.Agent.AspNetCore;
using Bucket.SkyApm.Transport.EventBus;
using Bucket.Utility;
using Bucket.Logging.Events;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;


namespace Bucket.Identity.WebApi
{
    /// <summary>
    /// 启动配置
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 初始化启动配置
        /// </summary>
        /// <param name="configuration">配置</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        /// <summary>
        /// 配置
        /// </summary>
        public IConfiguration Configuration { get; }
        /// <summary>
        /// 配置服务
        /// </summary>
        public void ConfigureServices(IServiceCollection services)
        {
            // 添加全家桶服务
            services.AddFamilyBucket(familyBucket =>
            {
                // 添加AspNetCore基础服务
                familyBucket.AddAspNetCore();
                // 添加授权认证Token参数
                familyBucket.Services.AddTokenJwtAuthorize(Configuration);
                // 添加授权认证
                familyBucket.AddApiJwtAuthorize();
                // 添加数据ORM、数据仓储
                familyBucket.AddSqlSugarDbContext().AddSqlSugarDbRepository();
                // 添加错误码服务
                familyBucket.AddErrorCodeServer();
                // 添加配置服务
                familyBucket.AddConfigServer();
                // 添加事件驱动
                familyBucket.AddEventBus(builder => { builder.UseRabbitMQ(); });
                // 添加服务发现
                familyBucket.AddServiceDiscovery(builder => { builder.UseConsul(); });
                // 添加负载算法
                familyBucket.AddLoadBalancer();
                // 添加链路追踪
                familyBucket.AddBucketSkyApmCore().UseEventBusTransport();
                // 添加日志消息总线传输
                familyBucket.AddLogEventTransport();
                // 添加工具组件
                familyBucket.AddUtil();
                // 添加组件定时任务
                familyBucket.AddAspNetCoreHostedService(builder => { builder.AddConfig().AddErrorCode(); });
                // 添加缓存组件
                familyBucket.AddCaching(build => { build.UseStackExchangeRedis(); });
                // 添加应用批量注册
                familyBucket.BatchRegisterService(Assembly.Load("Bucket.Identity.Services"), "Service", ServiceLifetime.Scoped);

            });
            // 添加过滤器
            services.AddMvc(option => { option.Filters.Add(typeof(WebApiActionFilterAttribute)); }).AddJsonOptions(options =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            // 添加接口文档
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "认证接口服务", Version = "v1" });
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Bucket.Identity.Dto.xml"));
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Bucket.Identity.WebApi.xml"));
                c.CustomSchemaIds(x => x.FullName);
                // Swagger验证部分
                c.AddSecurityDefinition("Bearer", new ApiKeyScheme { In = "header", Description = "请输入带有Bearer的Token", Name = "Authorization", Type = "apiKey" });
                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>> { { "Bearer", Enumerable.Empty<string>() } });
            });
            // 添加HttpClient管理
            services.AddHttpClient();
        }
        /// <summary>
        /// 配置请求管道
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // 文档
            ConfigSwagger(app);
            // 公共配置
            CommonConfig(app);
            // Welcome
            Console.WriteLine(Welcome());
        }
        /// <summary>
        /// 配置Swagger
        /// </summary>
        private void ConfigSwagger(IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "api v1");
            });
        }
        /// <summary>
        /// 公共配置
        /// </summary>
        private void CommonConfig(IApplicationBuilder app)
        {
            // 静态HttpContext使用
            app.UseStaticHttpContext();
            // 全局错误日志
            app.UseErrorLog();
            // 静态文件
            app.UseStaticFiles();
            // 路由
            ConfigRoute(app);
            // 服务注册
            app.UseConsulRegisterService(Configuration);
        }
        /// <summary>
        /// 路由配置,支持区域
        /// </summary>
        private void ConfigRoute(IApplicationBuilder app)
        {
            app.UseMvc(routes =>
            {
                routes.MapRoute("areaRoute", "view/{area:exists}/{controller}/{action=Index}/{id?}");
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
                routes.MapSpaFallbackRoute("spa-fallback", new { controller = "Home", action = "Index" });
            });
        }
        /// <summary>
        /// Welcome
        /// </summary>
        /// <returns></returns>
        private string Welcome()
        {
            var builder = new StringBuilder();
            builder.AppendLine("Initializing ...");
            builder.AppendLine();
            builder.AppendLine("***************************************************************");
            builder.AppendLine("*                                                             *");
            builder.AppendLine("*                Welcome To Bucket.Identity                   *");
            builder.AppendLine("*                                                             *");
            builder.AppendLine("***************************************************************");
            return builder.ToString();
        }
    }
}
