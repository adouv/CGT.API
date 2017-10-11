using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace CGT.DDD.Utils.Http {
    public static class StaticHttpContextExtensions {
        public static void AddHttpContextAccessor(this IServiceCollection services) {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        public static IApplicationBuilder UseStaticHttpContext(this IApplicationBuilder app) {
            var httpContextAccessor = app.ApplicationServices.GetRequiredService<IHttpContextAccessor>();
            MyHttpContext.Configure(httpContextAccessor);
            return app;
        }
    }
}
