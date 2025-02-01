using Cht.HMS.Web.API.DataManager;
using Cht.HMS.Web.API.DBConfiguration;
using Cht.HMS.Web.API.Manager;
using Cht.HMS.Web.API.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System.Text;

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

            app.UseCors("CorsPolicy");

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

            services.AddScoped<IRoleManager, RoleDataManager>();
            services.AddScoped<IConsultationDetailsManager, ConsultationDetailsDataManager>();
            services.AddScoped<IDoctorAssignmentManager, DoctorAssignmentDataManager>();
            services.AddScoped<IDoctorManager, DoctorDataManager>();
            services.AddScoped<ILabTestsManager, LabTestsDataManager>();
            services.AddScoped<IMedicalConsultationManager, MedicalConsultationDataManager>();
            services.AddScoped<IMedicinesManager, MedicinesDataManager>();
            services.AddScoped<IPatientRegistrationManager, PatientRegistrationDataManager>();
            services.AddScoped<IPatientTypeManager, PatientTypeDataManager>();
            services.AddScoped<IPaymentTypeManager, PaymentTypeDataManager>();
            services.AddScoped<IPharmacyManager, PharmacyDataManager>();
            services.AddScoped<IPrescriptionManager, PrescriptionDataManager>();
            services.AddScoped<IRadiologyManager, RadiologyDataManager>();
            services.AddScoped<IAuthenticationManager, AuthenticationDataManager>();
            services.AddScoped<IUserManager, UserDataManager>();

            var tokenKey = _configuration.GetValue<string>("tokenKey");

            /*services.AddScoped<IAuthenticationManager>(x=> new AuthenticationDataManager(tokenKey,x.GetRequiredServicere))*/
            ;

            var key = Encoding.ASCII.GetBytes(tokenKey);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateActor = false
                };
            });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                                   builder => builder
                                  .AllowAnyOrigin()
                                  .AllowAnyHeader()
                                  .AllowAnyMethod()
                                  .WithMethods("GET", "PUT", "DELETE", "POST", "PATCH")
                                  );
            });

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
                c.AddSecurityDefinition("Authorization", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter the Authorization header using the Bearer scheme.",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                 {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Authorization"
                                }
                            },
                            Array.Empty<string>()
                        }
                    });

            });
        }
    }
}
