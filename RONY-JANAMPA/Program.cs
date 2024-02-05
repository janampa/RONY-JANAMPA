using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using RONY_JANAMPA.Data;
using RONY_JANAMPA.Repository;
using RONY_JANAMPA.Repository.IRepository;
using RONY_JANAMPA.RetoMapper;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

//conexion DB

builder.Services.AddDbContext<ApplicationDbContext>(opciones =>
{
    opciones.UseSqlServer(builder.Configuration.GetConnectionString("conexion"));
});

// AGREGAR REPOSITORIOS

builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuariosRepository>();


//configuracion token
var key = builder.Configuration.GetValue<string>("Apisettings:secreata");

//AGREGAR AUTOMAPPER

builder.Services.AddAutoMapper(typeof(PedidoMapper));


//Configuarar la autenticación
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
}) ;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Soporte CORS
// se PONE * PARA PODER ACEPTAR CONEXION DESDE CUALQUIER RUTA
// EN LA PARTE DONDE ESTÁ LOCALHOSTO SE PUEDE PONER MAS DOMNIOS A DONDE DEBEN IR ESO DEBE ESTAR EN ,

builder.Services.AddCors(p => p.AddPolicy("PolicyCors", build =>
{
    build.WithOrigins("*").AllowAnyMethod().AllowAnyMethod().AllowAnyMethod();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//SOPORTE PARA CORSS

app.UseCors("PolicyCors");

app.UseAuthorization();
//agregar para autenticación
app.UseAuthentication();

app.MapControllers();

app.Run();
