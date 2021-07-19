using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using WebApi.Data;

namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Server=localhost;Database=tcc;Uid=root;Pwd=;
            var connection = Configuration["MySQLConnection:MySqlConnectionString"];

            services.AddDbContext<WebApiContextMySQL>(opt => opt.UseMySql(@connection, options => options.EnableRetryOnFailure()));
            services.AddScoped<WebApiContextMySQL, WebApiContextMySQL>();

            //services.AddDbContext<WebApiContext>(opt => opt.UseMySql(@"Server=localhost;Database=tcc;Uid=root;Pwd=;"));
            //services.AddScoped<WebApiContext, WebApiContext>();

            //services.AddDbContext<WebApiContext>(opt => opt.UseFirebird(@"database=localhost/7001:TCC.fdb;user=sysdba;password=masterkey"));
            //services.AddScoped<WebApiContext, WebApiContext>();

            /*services.AddCors(opt =>
            {
                opt.AddPolicy("Cors", builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
            });*/
            services.AddCors();
            services.AddControllers();


            var key = Encoding.ASCII.GetBytes(Settings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),

                    ValidateIssuer = false,
                    ValidateAudience = false,


                };
            });

            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ApiTestes", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiTestes v1"));
            }

            app.UseHttpsRedirection();



            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseCors(option => option
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
            );

            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
