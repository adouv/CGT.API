using Autofac;
using CGT.Api.DTO;
using CGT.DDD.IRepositories;
using CGT.Mongo.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CGT.Service
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
                .SingleInstance()
                .PropertiesAutowired();

            //mongodb仓储注入
            var mongo_model = Assembly.Load(new AssemblyName("CGT.Entity"));
            builder.RegisterAssemblyTypes(mongo_model)
                .Where(t => t.Name.EndsWith("MongoModel"))
                .AsSelf()
                .SingleInstance()
                .PropertiesAutowired();
            builder.RegisterGeneric(typeof(MongoRepository<>)).As(typeof(IMongoRepository<>));

            var apiservice = Assembly.Load(new AssemblyName("CGT.Base.Service"));
            builder.RegisterAssemblyTypes(apiservice)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces()
                .AsSelf()
                .SingleInstance()
                .PropertiesAutowired();

            // 注入空响应实体
            builder.RegisterType<ResponseMessageModel>().AsSelf().PropertiesAutowired();
            
            //注入总任务类
            builder.RegisterType<TaskQuest>().AsSelf().PropertiesAutowired();
        }
    }
}
