using TrainSystem.Controllers;
using TrainSystem.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddScoped<StationsRepository>();
builder.Services.AddScoped<StationsController>();

builder.Services.AddScoped<NewsRepository>();
builder.Services.AddScoped<NewsController>();

builder.Services.AddScoped<TicketsController>();
builder.Services.AddScoped<TicketsRepository>();

builder.Services.AddScoped<TrainsController>();
builder.Services.AddScoped<TrainsRepository>();

builder.Services.AddScoped<TripsController>();
builder.Services.AddScoped<TripsRepository>();

builder.Services.AddScoped<UsersController>();
builder.Services.AddScoped<UsersRepository>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();

}
else
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
