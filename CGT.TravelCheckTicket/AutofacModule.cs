using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using CGT.Mongo.Repositories;
using CGT.Api.DTO;
using CGT.DDD.IRepositories;
using CGT.DDD.Encrpty;
using CGT.DDD.Utils;
using Autofac;

namespace CGT.TravelCheckTicket
{
    public class AutofacModule : Autofac.Module
    {
        /// <summary>
        /// 加载注入
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder)
        {
            //petapoco仓储注入
            var petapocobase_repository = Assembly.Load(new AssemblyName("CGT.PetaPoco.Repositories"));
            builder.RegisterAssemblyTypes(petapocobase_repository)
                .Where(t => t.Name.EndsWith("Rep"))
                .AsSelf()
                .PropertiesAutowired();

            //mongodb仓储注入
            var mongo_model = Assembly.Load(new AssemblyName("CGT.Entity"));
            builder.RegisterAssemblyTypes(mongo_model)
                .Where(t => t.Name.EndsWith("MongoModel"))
                .AsSelf()
                .PropertiesAutowired();
            builder.RegisterGeneric(typeof(MongoRepository<>)).As(typeof(IMongoRepository<>));

            //apiservice业务注入
            builder.RegisterType<ResponseMessageModel>()
                .AsSelf()
                .PropertiesAutowired();

            var apiservice = Assembly.Load(new AssemblyName("CGT.Api.Service"));
            builder.RegisterAssemblyTypes(apiservice)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces()
                .AsSelf()
                .PropertiesAutowired();

            ////CGT.UserCenter.Service 业务注入
            var usercenterservice = Assembly.Load(new AssemblyName("CGT.UserCenter.Service"));
            builder.RegisterAssemblyTypes(usercenterservice)
                .Where(t => t.Name.EndsWith("Processor"))
                .AsImplementedInterfaces()
                .AsSelf()
                .PropertiesAutowired();

            //NodeEncrpty加解密服务注入
            builder.RegisterType<NodeEncrpty>()
                .AsSelf()
                .PropertiesAutowired();

            //注入Execl
            builder.RegisterType<ExeclHelper>()
                .AsSelf()
                .PropertiesAutowired();
        }
    }
}
