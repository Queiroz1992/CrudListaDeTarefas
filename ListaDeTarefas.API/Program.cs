using FluentValidation;
using FluentValidation.AspNetCore;
using ListaDeTarefas.Application;
using ListaDeTarefas.Application.Interfaces;
using ListaDeTarefas.Application.Validators;
using ListaDeTarefas.Domain;
using ListaDeTarefas.Infrastructure;
using ListaDeTarefas.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Registrar configurpação 
ConfigurationManager configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddControllers()
    .AddFluentValidation(fv =>
    {
        fv.RegisterValidatorsFromAssemblyContaining<TarefaValidator>();
        fv.RegisterValidatorsFromAssemblyContaining<SubTarefaValidator>();
    });

builder.Services.AddTransient<IValidator<Tarefa>, TarefaValidator>();
builder.Services.AddTransient<IValidator<SubTarefa>, SubTarefaValidator>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Adicionando Database Service 
builder.Services.AddDbContext<TarefaDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
     b => b.MigrationsAssembly("ListaDeTarefas.API")));

builder.Services.AddScoped<ITarefaRepository, TarefaRepository>();
builder.Services.AddScoped<ITarefaService, TarefaService>();
builder.Services.AddScoped<ISubTarefaService, SubTarefaService>();
builder.Services.AddScoped<ISubTarefaRepository, SubTarefaRepository>();

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
