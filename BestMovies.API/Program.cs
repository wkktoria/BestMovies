using System.Net;
using BestMovies.API.DBContexts;
using BestMovies.API.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BestMoviesContext>(
    o => o.UseSqlite(builder.Configuration.GetConnectionString("BestMoviesStr"))
);
builder.Services.AddIdentityApiEndpoints<IdentityUser>().AddEntityFrameworkStores<BestMoviesContext>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddProblemDetails();

builder.Services.AddAuthentication().AddJwtBearer();
builder.Services.AddAuthorization();
builder.Services.AddAuthorizationBuilder().AddPolicy("RequireAdminWithAppropriateName",
    policy => { policy.RequireRole("admin").RequireClaim("name", "Wiktoria"); });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("TokenAuthBestMovies",
        new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Description = "Token based on Authorization and Authentication",
            Type = SecuritySchemeType.Http,
            Scheme = "Bearer",
            In = ParameterLocation.Header
        });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "TokenAuthBestMovies"
                }
            },
            new List<string>()
        }
    });
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler(configureApplicationBuilder =>
    {
        configureApplicationBuilder.Run(async context =>
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "text/html";
            await context.Response.WriteAsync("An unexpected problem occurred");
        });
    });

    // app.UseExceptionHandler();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", () => "Today is: " + DateTime.Now).AllowAnonymous();

app.RegisterIdentityEndpoints();
app.RegisterMoviesEndpoints();
app.RegisterDirectorsEndpoints();

app.Run();