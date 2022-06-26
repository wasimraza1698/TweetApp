using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace TweetApp.Extensions
{
    public static class SwaggerConfig
    {
        public static void AddSwaggerGenConfig(this IServiceCollection services)
        {
            // This is to generate the Default UI of Swagger Documentation
            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1.0", new OpenApiInfo
                {
                    Title = "TweetApp",
                    Version = "v1.0",
                    Description = "ASP.NET 5.0 Core Web API for TweetApp"
                });

                // To Enable authorization using Swagger (JWT)
                config.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme()
                {
                    Name = "JWT Authentication",
                    Type = SecuritySchemeType.Http,
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter JW token only.",
                });

                config.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = JwtBearerDefaults.AuthenticationScheme
                                }
                            },
                            new string[] {}
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                config.IncludeXmlComments(xmlPath);
            });
        }
    }
}
