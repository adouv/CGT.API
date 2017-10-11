using Autofac;
using Autofac.Extensions.DependencyInjection;
using CGT.DDD.Utils.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.SwaggerUi;
using CGT.Api.Service;
using System.Text;

namespace CGT.Api {
    public class Startup {
        public IConfigurationRoot Configuration { get; }
        //注入Autofac容器
        public IContainer ApplicationContainer { get; private set; }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="env"></param>
        public Startup(IHostingEnvironment env) {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }
        public IServiceProvider ConfigureServices(IServiceCollection services) {
            //添加.net自带缓存
            services.AddMemoryCache();

            #region 注入获取HTTP上下文服务
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            #endregion

            #region 注入Node服务
            services.AddNodeServices(options => {
                var path = Directory.GetParent(Directory.GetCurrentDirectory());
                options.ProjectPath = path + @"\NodeEncrpty";
                options.WatchFileExtensions = new[] { ".js", ".sass" };
            });
            #endregion

            #region 跨域
            var urls = Configuration["AppConfig:Cores"].Split(',');
            services.AddCors(options =>
            options.AddPolicy("AllowSameDomain",
                              builder => builder.WithOrigins(urls).AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin().AllowCredentials())
             );
            #endregion

            #region API文档注入服务
            services.AddSwaggerGen(options => {
                options.SwaggerDoc("v1",
                    new Info {
                        Title = "CGT-Insure-Api",
                        Version = "v1",
                        Description = "A simple api to search using geo location in Elasticsearch",
                        TermsOfService = "None"
                    }
                 );

                var filePath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "CGT.Api.Controllers.xml");
                options.IncludeXmlComments(filePath);

                var requestfilePath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "CGT.Api.DTO.xml");
                options.IncludeXmlComments(requestfilePath);
                options.DescribeAllEnumsAsStrings();

            });
            #endregion

            #region MVC修改控制器描述
            services.AddHttpContextAccessor();
            services.Replace(ServiceDescriptor.Transient<IControllerActivator, ServiceBasedControllerActivator>());
            services.AddMvc(config => {
                config.RespectBrowserAcceptHeader = true;
            })
            .AddJsonOptions(options => options.SerializerSettings.ContractResolver = new ContractResolver())
            .AddJsonOptions(options => options.SerializerSettings.Converters.Add(new ChinaDateTimeConverter()))
            .AddFormatterMappings(options => options.SetMediaTypeMappingForFormat("xlsx", new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")))
            .AddFormatterMappings(options => options.SetMediaTypeMappingForFormat("jpeg", new MediaTypeHeaderValue("image/jpeg")))
            .AddFormatterMappings(options => options.SetMediaTypeMappingForFormat("jpg", new MediaTypeHeaderValue("image/jpeg")));

            #endregion

            #region Autofac配置
            var autofac = new ContainerBuilder();
            var assembly = Assembly.Load(new AssemblyName("CGT.Api.Controllers"));
            var manager = new ApplicationPartManager();

            manager.ApplicationParts.Add(new AssemblyPart(assembly));
            manager.FeatureProviders.Add(new ControllerFeatureProvider());

            var feature = new ControllerFeature();

            manager.PopulateFeature(feature);

            autofac.RegisterType<ApplicationPartManager>().AsSelf().SingleInstance();
            autofac.RegisterTypes(feature.Controllers.Select(ti => ti.AsType()).ToArray()).PropertiesAutowired();

            autofac.RegisterModule(new AutofacModule());
            autofac.Populate(services);
            this.ApplicationContainer = autofac.Build();
            #endregion

            #region AutoMapper
            AutoMapperConfiguration.RegisterMappings();
            #endregion
            //设置支持gb2312
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            return new AutofacServiceProvider(this.ApplicationContainer);
        }


        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IServiceProvider svp) {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            CGTHttpContext.ServiceProvider = svp;
            app.UseCors("AllowSameDomain");
            app.UseStaticFiles();
            app.UseStaticHttpContext();

            #region API文档中间件
            app.UseSwagger(c => {
                c.PreSerializeFilters.Add((swagger, httpReq) => swagger.Host = httpReq.Host.Value);
            });
            app.UseSwaggerUi(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "CGTInsure-API v1.0.0");
                c.ShowRequestHeaders();
            });
            #endregion

            app.UseMvc();
        }
    }
}
