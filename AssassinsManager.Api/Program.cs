using AssassinsManager.Api;
using AssassinsManager.Api.Controllers;
using AssassinsManager.Api.Services;
using AssassinsManager.Core.Services;
using AssassinsManager.Core.Services.Interfaces;
using AssassinsManager.EntityFramework;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json");
builder.Configuration.AddEnvironmentVariables();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<AssassinCoreContext>(options => {
    options.UseMySQL(builder.Configuration.GetConnectionString("MySql"));
});

builder.Services.AddSingleton<ISchedulerService, SchedulerService>();
builder.Services.AddSingleton<EventService>();
builder.Services.AddScoped<BlogController>(); // Apparently adding this as a controller doesn't let me access it?
builder.ConfigureQuartz();
builder.Services.AddControllers();

builder.Services.AddIdentityApiEndpoints<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AssassinCoreContext>();

builder.Services.AddAuthentication().AddGoogle(googleOptions =>
{
    googleOptions.ClientId = builder.Configuration["Authentication:Google:ClientId"];
    googleOptions.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
});
builder.Services.AddAuthorizationBuilder();

var app = builder.Build();

app.MapIdentityApi<IdentityUser>();

app.Services.GetRequiredService<ISchedulerService>();
app.Services.GetRequiredService<EventService>();

app.MigrateDatabase<AssassinCoreContext>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.UseAuthentication();
app.UseForwardedHeaders();
if(app.Environment.IsDevelopment())
    app.MapControllers().AllowAnonymous();
else
    app.MapControllers();

app.Run();