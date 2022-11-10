using Main;
using Main.Data;
using Main.Logging;
using Main.Repository.IRepository;
using Main.Repository;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors();

#region Use Serilog
//Log.Logger = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.File("log/villaLogs.txt", rollingInterval: RollingInterval.Day).CreateLogger();
//builder.Host.UseSerilog();
#endregion

// �إ߸�Ʈw�s�u
builder.Services.AddDbContext<ApplicationDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLConnection"));
});

// AutoMapper�]�w
builder.Services.AddAutoMapper(typeof(AutoMapperConfig));

builder.Services.AddControllers(option => { }).AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region ���UDI(�޲z�Ҧ�Repository)
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
#endregion

#region Custom Log
//builder.Services.AddSingleton<ILogging, Logging>();
#endregion

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
