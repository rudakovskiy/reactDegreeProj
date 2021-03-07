using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;
using System.Reflection;
using GreenHealthApi.Endpoints.Customers;
using GreenHealthApi.Endpoints.Images;
using GreenHealthApi.Endpoints.Medicaments;
using GreenHealthApi.Endpoints.Orders;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

//TODO add auth attrib to whole controllers
namespace GreenHealthApi
{
    public class Startup
    {
        private readonly string _dbConString;
        private readonly string _hostUrl;
        
        
        public Startup(IConfiguration configuration)
        {
            var envString = configuration["DB"];
            _dbConString = string.IsNullOrEmpty(envString)
                ? @"Server=localhost\SQLEXPRESS;Database=master;Trusted_Connection=True;"
                : envString;
            
            var hostUrlEnv = configuration["hosturl"];
            _hostUrl = string.IsNullOrEmpty(hostUrlEnv) ? @"http://localhost:80" : hostUrlEnv;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(c => c.AddDefaultPolicy(builder =>
            {
                builder.WithOrigins("http://localhost:3000", "http://localhost:80");
                builder.AllowAnyMethod();
                builder.AllowAnyHeader();
                builder.AllowCredentials();
            }));
            services.AddControllers();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // укзывает, будет ли валидироваться издатель при валидации токена
                        ValidateIssuer = true,
                        // строка, представляющая издателя
                        ValidIssuer = AuthOptions.ISSUER,
 
                        // будет ли валидироваться потребитель токена
                        ValidateAudience = true,
                        // установка потребителя токена
                        ValidAudience = AuthOptions.AUDIENCE,
                        // будет ли валидироваться время существования
                        ValidateLifetime = false,
 
                        // установка ключа безопасности
                        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                        // валидация ключа безопасности
                        ValidateIssuerSigningKey = true,
                    };
                });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Fixe API",
                    Description = "Fixe API ASP.NET Core Web API",
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. Bearer {token}"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference 
                            { 
                                Type = ReferenceType.SecurityScheme, 
                                Id = "Bearer" 
                            }
                        },
                        new string[] {}
                    }
                });
                // Set the comments path for the Swagger JSON and UI.
                /*var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);*/
            });
        
            
            var connectionStringGetter = new StaticConnectionStringGetter(_dbConString);
            services.AddTransient<IConnectionStringGetter>( con => connectionStringGetter);

            var contentRootUrlGetter = new StaticContentRootUrlGetter(_hostUrl + @"/staticImages");
            services.AddTransient<IStaticContentRootUrlGetter>(crg => contentRootUrlGetter);

            #region Writers
            services.AddTransient<IMedicamentWriter, MedicamentWriter>();
            services.AddTransient<IImageWriter, ImageWriter>();
            services.AddTransient<ICustomerWriter, CustomerWriter>();
            services.AddTransient<IOrdersWriter, OrdersWriter>();
            #endregion

            #region Readers
            services.AddTransient<IMedicamentReader, MedicamentReader>();
            services.AddTransient<IImageReader, ImageReader>();
            services.AddTransient<ICustomerReader, CustomerReader>();
            services.AddTransient<IOrdersReader, OrdersReader>();

            #endregion


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(
                    env.ContentRootPath),
                RequestPath = new PathString("/staticImages")
            });
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors();
            
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
        }
    }
}
