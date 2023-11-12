using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DbContext>(builder =>
                {
                  builder.UseNpgsql("");
                  builder.UseOpenIddict();
                });
builder.Services.AddOpenIddict()
                .AddCore(builder => builder.UseEntityFrameworkCore())
                .AddServer(builder =>
                {
                  builder.SetTokenEndpointUris("connect/token");
                  builder.AllowClientCredentialsFlow();
                  builder.UseAspNetCore()
                         .EnableTokenEndpointPassthrough();
                })
                .AddValidation(builder => builder.UseAspNetCore());

WebApplication app = builder.Build();
app.Run();
