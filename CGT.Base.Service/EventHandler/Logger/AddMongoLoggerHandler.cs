using CGT.DDD.Events;
using CGT.Entity.MongoModel;
using CGT.Event.Model;
using CGT.Mongo.Repositories;

namespace CGT.Base.Service.EventHandler.Logger
{
    /// <summary>
    /// 添加mongodb日志
    /// </summary>
    /// <typeparam name="AddLoggerEvent"></typeparam>
    public class AddMongoLoggerHandler : IEventHandler<AddMongoLoggerEvent>
    {
        /// <summary>
        /// mongodb日志
        /// </summary>
        public void Handle(AddMongoLoggerEvent evt)
        {
            var mongodbLoggger = new MongoRepository<ApiLoggerMongoModel>();

            mongodbLoggger.Insert(new ApiLoggerMongoModel()
            {
                GuidKey = evt.GuidKey,
                RequestDeData = evt.RequsetDeData,
                RequestEnData = evt.RequestEnData,
                ActionNmae = evt.ActionNmae,
                Code = evt.Code,
                CreateTime = evt.CreateTime,
                Mesasge = evt.Mesasge,
                ResponseData = evt.ResponseData,
                StackTrace = evt.StackTrace
            });
        }

    }
}
