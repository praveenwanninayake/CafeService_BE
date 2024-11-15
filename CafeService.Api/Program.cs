using CafeService.Api.Context;
using CafeService.Api.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//set database
builder.Services.AddDbContext<AppDbContext>(config =>
{
    string mySqlConnectionStr = builder.Configuration.GetConnectionString("DefaultConnection");
    config.UseMySql(mySqlConnectionStr, ServerVersion.AutoDetect(mySqlConnectionStr));

});




builder.Services.AddScoped<ICafeService, CafeServiceImplement>();
builder.Services.AddScoped<IEmployeeService, EmployeeServiceImplement>();





builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
