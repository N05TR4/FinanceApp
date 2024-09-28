using FinanceApp.Infraestructure.Context;
using FinanceApp.IOC.Dependencies;
using Microsoft.EntityFrameworkCore;



var builder = WebApplication.CreateBuilder(args);

// Registrar AutoMapper y escanear el ensamblado para encontrar perfiles de mapeo
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Add services to the container.


builder.Services.AddDbContext<FinanceAppDbContext>(option =>
                                                   option.UseSqlServer(
                                                   builder.Configuration
                                                   .GetConnectionString("FinanceApp")
                                                   ));

// Dependency Injection
builder.Services.AddCategoriaDependency();
builder.Services.AddTipoDependency();
builder.Services.AddGastoDependency();
builder.Services.AddIngresoDependency();
builder.Services.AddMetodoPagoDependency();
builder.Services.AddUsuarioDependency();

builder.Services.AddControllers();


builder.Services.AddCors(opt =>
{
    opt.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod();
        });

});


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
app.UseCors("AllowSpecificOrigin");

app.UseAuthorization();

app.MapControllers();

app.Run();
