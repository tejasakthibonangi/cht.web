using Cht.HMS.Web.API.DBConfiguration;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace Cht.HMS.Web.API
{
    public class Startup
    {

        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            //app.UseCors("CorsPolicy");


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

            });

            app.UseStaticFiles();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cht.HMS.Web.API");
            });

        }
        public void ConfigureServices(IServiceCollection services) 
        {
            services.AddDbContext<ApplicationDBContext>(options => options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"),
                                                       sqlServerOptionsAction: sqlOptions =>
                                                       {
                                                           sqlOptions.EnableRetryOnFailure(
                                                               maxRetryCount: 5,
                                                               maxRetryDelay: TimeSpan.FromSeconds(30),
                                                               errorNumbersToAdd: null);
                                                       }));

            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            });


            services.AddMvc().AddXmlSerializerFormatters();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Cht.HMS.Web.API",
                    Description = "Cht.HMS.Web.API",
                    Contact = new OpenApiContact
                    {
                        Name = "Cht.HMS.Web.API Service",
                        Email = "tejasakthi.bonangi@yahoo.com",
                        Url = new Uri("https://test.in")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "tejasakthi.bonangi@yahoo.com",
                        Url = new Uri("https://test.in")
                    }
                });
               
            });
        }
    }
}
