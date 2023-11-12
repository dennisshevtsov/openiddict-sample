using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<DbContext>(options => options.UseOpenIddict());
builder.Services.AddOpenIddict()
                .AddCore(options => options.UseEntityFrameworkCore())
                .AddServer(options =>
                {
                  options.SetTokenEndpointUris("connect/token");
                  options.AllowClientCredentialsFlow();
                  options.UseAspNetCore()
                         .EnableTokenEndpointPassthrough();
                })
                .AddValidation(options => options.UseAspNetCore());

WebApplication app = builder.Build();
app.Run();
