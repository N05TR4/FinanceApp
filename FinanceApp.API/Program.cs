using FinanceApp.Infraestructure.Configuration;
using FinanceApp.Infraestructure.Context;
using FinanceApp.IOC.Dependencies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;



var builder = WebApplication.CreateBuilder(args);




/// Registrar la configuración de JwtSettings
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

// Configurar JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Secret"]))
    };
});


// Registrar AutoMapper y escanear el ensamblado para encontrar perfiles de mapeo
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Add services to the container.

// Conexión a la base de datos
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
