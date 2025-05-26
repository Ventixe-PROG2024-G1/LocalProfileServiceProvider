using LocalProfileServiceProvider.Data.Contexts;
using LocalProfileServiceProvider.Data.Repositories;
using LocalProfileServiceProvider.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddGrpc(x =>
//    x.EnableDetailedErrors = true);
builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddDbContext<ProfileDbContext>(x =>
        x.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionStringData")));

builder.Services.AddScoped<IUserProfileRepo, UserProfileRepo>();
builder.Services.AddScoped<UserProfileService>();
var app = builder.Build();

//app.MapGrpcService<UserProfileService>();
app.MapGet("/", () => "EmailServiceProvider is running.");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();