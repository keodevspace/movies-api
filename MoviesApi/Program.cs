using Microsoft.EntityFrameworkCore;
using MoviesApi.Data;
using MoviesApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Adiciona serviços ao contêiner.
builder.Services.AddControllers();

// Configura o Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registra o DbContext com SQLite
builder.Services.AddDbContext<MoviesContext>(options =>
    options.UseSqlite("Data Source=./Database/movies.db"));

// Registra o MovieService como Scoped
builder.Services.AddScoped<MovieService>();

// Configura CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Aplica as migrações pendentes ao banco de dados
using (var scope = app.Services.CreateScope())
    {
    var dbContext = scope.ServiceProvider.GetRequiredService<MoviesContext>();
    dbContext.Database.Migrate();
    }

// Configura o pipeline de requisição HTTP.
if (app.Environment.IsDevelopment())
    {
    app.UseSwagger();
    app.UseSwaggerUI();
    }

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("AllowAll");

app.MapControllers();

app.Run();
