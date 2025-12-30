using Microsoft.EntityFrameworkCore;
using ProjetoMei.Data;
using ProjetoMei.Interfaces;
using ProjetoMei.Repository;
using ProjetoMei.Services;
using System;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddTransient<IUsuarioMeiService, UsuarioMeiService>();
builder.Services.AddTransient<IUsuarioMeiRepository, UsuarioMeiRepository>();


builder.Services.AddDbContext<MeiDbContext>(options =>
{
    options.UseInMemoryDatabase("MyDatabase");
});







//var connectionString = builder.Configuration.GetConnectionString("AppDbConnectionString");

//builder.Services.AddDbContext<MeiDbContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
