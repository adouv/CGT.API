using Autofac;
using Autofac.Extensions.DependencyInjection;
using CGT.DDD.Config;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace CGT.Service {
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        //注入Autofac容器
        public IContainer ApplicationContainer { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            string service_db = JsonConfig.JsonRead("cgtHangfireConnection");
            services.AddHangfire(r => r.UseSqlServerStorage(service_db));
            services.AddMvc();

            #region Autofac配置
            var autofac = new ContainerBuilder();
            autofac.RegisterModule(new AutofacModule());
            autofac.Populate(services);
            this.ApplicationContainer = autofac.Build();
            #endregion

            return new AutofacServiceProvider(this.ApplicationContainer);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IServiceProvider svp)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseStaticFiles();

            var options = new DashboardOptions
            {
                Authorization = new[] { new HangfireAuthorizationFilter() }
            };

            app.UseHangfireServer();
            app.UseHangfireDashboard("/hangfire", options);

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            var TaskFactory = svp.GetService<TaskQuest>();
            TaskFactory.Reset();
            TaskFactory.InitTaskPage();
            TaskFactory.InitTaskList();
        }
    }
}
