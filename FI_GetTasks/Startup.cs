using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using Elastic.Apm.AspNetCore;
using FI_Infra_Implementation;
using FI_Infra_Tools_Aggregate;
using FI_Infra_Tools_Implementation;
using FI_Infra_Tools_Core;
using FI_Infra_Core;
using FI_Domain_Aggregate;
using FI_Aplication_Aggregate;
using FI_Aplication_Implementation;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using FI_Aplication_Events_Domain;
using FI_Domain;
using System.Reflection;
using MediatR;

namespace FIAPI;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddMediatR(typeof(MediatrDomainEventDispatcher).GetTypeInfo().Assembly);
        services.AddTransient<IDomainEventDispatcher, MediatrDomainEventDispatcher>();
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAllHeaders",
                  builder =>
                  {
                      builder.AllowAnyOrigin()
                             .AllowAnyHeader()
                             .AllowAnyMethod();
                  });
        });
        services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();


        services.AddDbContext<MainContext>(options =>

              options.UseSqlServer(Configuration.GetConnectionString("databaseTest"), opt => opt.EnableRetryOnFailure())
         );
        services.AddScoped(typeof(IContextWorkUnit), typeof(MainContext));
        services.AddScoped(typeof(IGetTask), typeof(GetTasksRepository));
        services.AddScoped(typeof(IGetTasksService), typeof(GetTasksService));
        services.AddScoped(typeof(IToken), typeof(TokenService));
        services.AddScoped(typeof(ILog), typeof(LogService));
        services.AddScoped(typeof(IEmail), typeof(FI_Infra_Tools_Implementation.Email));
        services.AddScoped(typeof(IDevOps), typeof(DevOps));
        var token = Configuration.GetSection("tokenManagement").Get<TokenManagement>();
        services.AddSingleton(token);
        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = true;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = token.Issuer,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(token.Secret)),
                ValidAudience = token.Audience,
                ValidateAudience = false
            };
        });

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "API Test",
                Description = "Update task data"

            });


       
            var securityScheme = new OpenApiSecurityScheme
            {
                Name = "Autenticacion",
                Description = "Introduzca el token",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer", 
                BearerFormat = "JWT",
                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };
            c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                    {securityScheme, new string[] { }}
            });


        });


        services.AddControllers();






    }


    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseCors("AllowAllHeaders");

        app.UseHsts();
        app.UseHttpsRedirection();

        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("v1/swagger.json", "Versioned API v1.0");

            c.DocumentTitle = "Documentacion";
            c.DocExpansion(DocExpansion.None);
        });


        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseElasticApm(Configuration);
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });



    }
}

