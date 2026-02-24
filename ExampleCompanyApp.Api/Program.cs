using Autofac;
using Autofac.Extensions.DependencyInjection;
using ExampleCompanyApp.Api.AuthenticationService;
using ExampleCompanyApp.Api.Modules;
using ExampleCompanyApp.Caching;
using ExampleCompanyApp.Core.Services;
using ExampleCompanyApp.Repository;
using ExampleCompanyApp.Service.Mapping;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "ExampleCompanyApp API",
        Version = "v1"
    });

    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: ",
        Name = "Authorization",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});


builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
     {
         var config = builder.Configuration.GetSection("Jwt");
         options.TokenValidationParameters = new
         Microsoft.IdentityModel.Tokens.TokenValidationParameters
         {
             ValidateIssuer = true,
             ValidateAudience = true,
             ValidateLifetime = true,
             ValidIssuer = config["Issuer"],
             ValidAudience = config["Audience"],
             IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey
             (System.Text.Encoding.UTF8.GetBytes(config["Key"])),
             ValidateIssuerSigningKey = true
         };
     });


builder.Services.AddMemoryCache();

builder.Services.AddScoped<IJwtService, JwtService>();

//builder.Services.AddScoped(typeof(NotFoundFilter))

//builder.Services.AddScoped<IProductService, ProductServiceWithCaching>();

builder.Services.AddAutoMapper(c =>
{
    c.AddProfile<MapProfile>();
});

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder => containerBuilder.RegisterModule(new RepoServiceModule()));

builder.Services.AddDbContext<ExampleCompanyDbContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), option =>
    {
        option.MigrationsAssembly("ExampleCompanyApp.Repository");
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
