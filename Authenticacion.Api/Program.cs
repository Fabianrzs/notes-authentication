using Authentication.Infrastrunture;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddControllers();

var app = builder.Build();
app.UseInfrastructure(app.Environment);

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
