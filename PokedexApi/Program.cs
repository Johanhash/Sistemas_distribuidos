using PokedexApi.Repositories;
using PokedexApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Grpc.Net.Client;
using PokedexApi.Infraestructure.Grpc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddScoped<IPokemonService, PokemonService>();
builder.Services.AddScoped<IPokemonRepository, PokemonRepository>();
builder.Services.AddScoped<ITrainerRepository, TrainerRepository>();
builder.Services.AddScoped<ITrainerService, PokedexApi.Services.TrainerService>();

builder.Services.AddSingleton(s =>
{
    var channel = GrpcChannel.ForAddress(builder.Configuration.GetValue<string>("TrainersApiUrl")!);

    return new PokedexApi.Infraestructure.Grpc.TrainerService.TrainerServiceClient(channel);
});

builder.Services.AddScoped<IHobbyService, HobbyService>();
builder.Services.AddScoped<IHobbyRepository, HobbyRepository>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = builder.Configuration.GetValue<string>("Authentication:Authority");
        options.RequireHttpsMetadata = false; 
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration.GetValue<string>("Authentication:Issuer"),
            ValidateLifetime = true,
            ValidateAudience = true,
            ValidAudience = "pokedex-api",
            ValidateIssuerSigningKey = true
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Read", policy => policy.RequireClaim("http://schemas.microsoft.com/identity/claims/scope", "read"));
    options.AddPolicy("Write", policy => policy.RequireClaim("http://schemas.microsoft.com/identity/claims/scope", "write"));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();