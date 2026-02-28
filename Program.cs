using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Interfaces;
using WebAPI.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


builder.Services.AddDbContext<ApplicationDBContext>(options => //aggiunge il servizio del contesto del database all'interno del contenitore di dipendenza dell'applicazione, c
                                                               //onsentendo di utilizzare il contesto per accedere al database e gestire le operazioni di accesso ai dati
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")); //configura il contesto del database per utilizzare SQL Server,
                                                                                          //specificando la stringa di connessione "DefaultConnection" che viene recuperata dalla
                                                                                          //configurazione dell'applicazione
});

builder.Services.AddScoped<IClientiRepository, ClientiRepository>();
builder.Services.AddScoped<IOperazioniRepository, OperazioniRepository>();
//builder.Services.AddScoped<IUtentiRepository, UtentiRepository>();

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
