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
            // Makes sure no need to add Authorize attribute
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

        public class AuthorizeRolesAttribute : AuthorizeAttribute
        {
            public AuthorizeRolesAttribute(params Roles[] roles) : base()
            {
                Roles = string.Join(",", roles);
            }
        }

    }

}

