using Canteen.Core.Managers;
using Canteen.Core.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Canteen.API.Middlewares
{
    public static class Authentication
    {
        public static void AddJwtAuthentication(this IServiceCollection services, string secret, string issuer, string audience)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = TokenValidationParams(secret, issuer, audience);
            });

            // ensure that authorization is required globally throughout the app
            var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();

            services.AddControllers(options => { options.Filters.Add(new AuthorizeFilter(policy)); })
            .AddJsonOptions(options => { options.JsonSerializerOptions.IgnoreNullValues = true; });
        }

        public static IApplicationBuilder UseJwtAuthentication(this IApplicationBuilder app)
        {
            return app.UseMiddleware<JwtMiddleware>();
        }

        private static TokenValidationParameters TokenValidationParams(string secret, string issuer, string audience)
        {
            return new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = issuer,
                ValidAudience = audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
                RequireExpirationTime =true,
                ClockSkew = TimeSpan.Zero
            };
        }
}

    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly Jwt _settings;

        public JwtMiddleware(RequestDelegate next, IOptions<Jwt> settings)
        {
            _next = next;
            _settings = settings.Value;
        }

        public async Task Invoke(HttpContext context, IAuthManager auth)
        {
            // retrieve token from cookie 
            var token = context.Request.Cookies["token"];
            if (string.IsNullOrEmpty(token) == false)
            {
                context.Request.Headers.Add("Authorization", "Bearer " + token);
            }

            await _next(context);
        }
    }
}

