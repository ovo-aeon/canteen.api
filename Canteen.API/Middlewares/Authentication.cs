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
using System.IdentityModel.Tokens.Jwt;
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
        //public static IApplicationBuilder UseJwtAuthentication(this IApplicationBuilder app)
        //{
        //    return app.UseMiddleware<JwtMiddleware>();
        //}
    }

    //public class JwtMiddleware
    //{
    //    private readonly RequestDelegate _next;
    //    private readonly Jwt _settings;

    //    public JwtMiddleware(RequestDelegate next, IOptions<Jwt> settings)
    //    {
    //        _next = next;
    //        _settings = settings.Value;
    //    }

    //    public async Task Invoke(HttpContext context, IAuthManager mgr)
    //    {
    //        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

    //        if (token != null)
    //            attachUserToContext(context, mgr, token);

    //        await _next(context);
    //    }

    //    private void attachUserToContext(HttpContext context, IAuthManager mgr, string token)
    //    {
    //        try
    //        {
    //            var tokenHandler = new JwtSecurityTokenHandler();
    //            var key = Encoding.ASCII.GetBytes(_settings.Key);
    //            tokenHandler.ValidateToken(token, new TokenValidationParameters
    //            {
    //                ValidateIssuerSigningKey = true,
    //                IssuerSigningKey = new SymmetricSecurityKey(key),
    //                ValidateIssuer = false,
    //                ValidateAudience = false,
    //                // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
    //                ClockSkew = TimeSpan.Zero
    //            }, out SecurityToken validatedToken);

    //            var jwtToken = (JwtSecurityToken)validatedToken;
    //            var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

    //            // attach user to context on successful jwt validation
    //            context.Items["User"] = mgr.GetUser(userId);
    //        }
    //        catch
    //        {
    //            // do nothing if jwt validation fails
    //            // user is not attached to context so request won't have access to secure routes
    //        }
    //    }
    //}
}

