using System.Text;
using api.Context.UnitOfWork;
using api.Middleware;
using api.Services.Account;
using api.Services.Posts;
using api.Services.Token;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace api.Extensions
{
    public static class DIExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection service, IConfiguration config)
        {
            service.AddControllers();
            service.AddEndpointsApiExplorer();
            service.AddSwaggerGen();
            service.AddHttpContextAccessor();

            service.AddAuthentication()
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.TokenValidationParameters = new()
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(config["AuthOptions:TokenSecret"]!)),
                        ValidateAudience = false,
                        ValidateIssuer = false
                    };

                    // This is used to populate the User.Identity
                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var authCookieName = config["AuthOptions:AuthCookieName"];

                            if (context.Request.Cookies.ContainsKey(authCookieName))
                                context.Token = context.Request.Cookies[authCookieName];

                            return Task.CompletedTask;
                        }
                    };
                });

            service
                .AddScoped<GlobalExceptionMiddleware>()
                .AddScoped<ITokenService, TokenService>()
                .AddScoped<IUnitOfWork, UnitOfWork>()
                .AddScoped<IPostsService, PostsService>()
                .AddScoped<IAccountService, AccountService>()
                .AddTransient<IPasswordService, PasswordService>();

            return service;
        }
    }
}