using Microsoft.EntityFrameworkCore;
using PokemonApi.Services;
using PokemonApi.Infrastructure;
using PokemonApi.Repositories;
using SoapCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSoapCore();
//TODO: CHANGE FROM SCOPED TO SINGLENTON
builder.Services.AddSingleton<IPokemonService, PokemonService>();
builder.Services.AddScoped<IPokemonRepository, PokemonRepository>();


builder.Services.AddDbContext<RelationalDbContext>(options => options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));

//builder.Services.AddScoped<>(); Crea una nueva instancia del servicio se crea una vez por cada solicitud HTTP y se comparte en todos los componentes que lo soliciten durante esta misma solicitud
//builder.Services.AddTransient<>(); Crea una nueva instancia del servicio se crea cada vez que se solicita
//builder.Services.AddSingleton<>(); Una unica instancia del servicio se crea y se comparte en toda la aplicacion 

var app= builder.Build();

app.UseSoapEndpoint<IPokemonService>("/PokemonService.svc", new SoapEncoderOptions());

app.Run();
