using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<DbContext>(options =>
{
  options.UseOpenIddict();
});
builder.Services.AddOpenIddict()
                .AddServer();

WebApplication app = builder.Build();
app.Run();
