using ARMApocalypseSASAPI.Data;
using ARMApocalypseSASAPI.Helpers;
using ARMApocalypseSASAPI.Interfaces;
using ARMApocalypseSASAPI.Mappers;
using ARMApocalypseSASAPI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<ICoreService, CoreService>();
builder.Services.Configure<ApiConfig>(builder.Configuration.GetSection(nameof(ApiConfig)));
builder.Services.AddAutoMapper(typeof(ProfileMapper));
builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;

        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;

    })
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                })
                ;

builder.Services.AddApiVersioning(o =>
{
    o.ReportApiVersions = true;
    o.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    o.AssumeDefaultVersionWhenUnspecified = true;
});

var swaggerConfigSection = builder.Configuration.GetSection(nameof(SwaggerConfig));
builder.Services.Configure<SwaggerConfig>(swaggerConfigSection);
var swaggerConfigSettings = swaggerConfigSection.Get<SwaggerConfig>();

builder.Services.AddSwaggerGen(
    options =>
    {
        //options.ExampleFilters();
        options.DescribeAllParametersInCamelCase();
        options.EnableAnnotations();
        options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "ZSSN API", Version = "v1" });
        options.AddServer(new OpenApiServer()
        {
            Url = swaggerConfigSettings.OpenApiServerURI //"https://xxx.xxx.xxx.xxx/xxx"
        });

        //var securityScheme = new OpenApiSecurityScheme
        //{
        //    Name = "JWT Authentication",
        //    Description = "Enter JWT Bearer token **_only_**",
        //    In = ParameterLocation.Header,
        //    Type = SecuritySchemeType.Http,
        //    Scheme = "bearer", // must be lower case
        //            BearerFormat = "JWT",
        //    Reference = new OpenApiReference
        //    {
        //        Id = JwtBearerDefaults.AuthenticationScheme,
        //        Type = ReferenceType.SecurityScheme
        //    },


        //};
        //options.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
        //options.AddSecurityRequirement(new OpenApiSecurityRequirement
        //    {
        //                {securityScheme, new string[] { }}
        //    });


    }
    ).AddSwaggerGenNewtonsoftSupport();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    var prefix = string.IsNullOrWhiteSpace(c.RoutePrefix) ? "." : "..";
    c.SwaggerEndpoint($"{prefix}/swagger/v1/swagger.json", "ZSSN API");


});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
