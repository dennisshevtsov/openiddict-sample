WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenIddict()
                .AddServer();

WebApplication app = builder.Build();
app.Run();
