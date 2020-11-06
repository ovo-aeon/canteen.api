using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Canteen.Core.DataAccess;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Canteen.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Canteen.API.Middlewares;
using Canteen.Core.Managers;
using Canteen.Core.Utilities;

namespace Canteen.API
{
    public class Startup
    {
        private Jwt _settings { get; }
        //private IWebHostEnvironment _env { get; }
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            _settings = configuration.GetSection("Jwt").Get<Jwt>();
            //_env = env;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            // register data access implementation
            services.AddDbContextPool<CanteenContext>(options => 
            options.UseSqlServer(
                Configuration.GetConnectionString("Canteen.DefaultConnection"),
                ctx => ctx.MigrationsAssembly(typeof(CanteenContext).Assembly.FullName)));
           // 
            services.AddUnitOfWork<CanteenContext>();
            services.AddJwtAuthentication(_settings.Key, _settings.Issuer, _settings.Audience);
           
            // Managers
            services.AddScoped<IAuthManager, AuthManager>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, CanteenContext context)
        {
            context.Database.Migrate();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            // global cors policy
            //app.UseCors(x => x
            //    .AllowAnyOrigin()
            //    .AllowAnyMethod()
            //    .AllowAnyHeader());
            app.UseRouting();
            app.UseJwtAuthentication();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSerilogRequestLogging();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
