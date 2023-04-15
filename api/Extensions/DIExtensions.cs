using api.Context;
using api.Context.UnitOfWork;
using api.Middleware;
using api.Repositories;
using api.Services;

namespace api.Extensions
{
    public static class DIExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection service)
        {
            service.AddControllers();
            service.AddEndpointsApiExplorer();
            service.AddSwaggerGen();

            service
                .AddScoped<GlobalExceptionMiddleware>()
                .AddSingleton<ISqlContext, SqlContext>()
                .AddScoped<IUnitOfWork, UnitOfWork>()
                .AddScoped<IPostsService, PostsService>();

            return service;
        }
    }
}