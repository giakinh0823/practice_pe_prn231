using AutoMapper;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using PE_BEST_PRACTICE.Dto;
using PE_BEST_PRACTICE.Models;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// add sql server
builder.Services.AddDbContext<PE_PRN_Fall22B1Context>(options => options.UseSqlServer
    (builder.Configuration.GetConnectionString("SqlConnection")
    ?? throw new InvalidOperationException("Connection string 'SqlConnection' not found.")));

// add odata
static IEdmModel GetEdmModel()
{
    ODataConventionModelBuilder builder = new();
    builder.EntitySet<Movie>("Movies");
    builder.EntitySet<Producer>("Producers");
    builder.EntitySet<Genre>("Genres");
    builder.EntitySet<Director>("Directors");
    builder.EntitySet<Star>("Stars");
    return builder.GetEdmModel();
}

builder.Services.AddControllers().AddOData(option => option.Select().Filter().Count().OrderBy().Expand().SetMaxTop(100)
                .AddRouteComponents("odata", GetEdmModel()));

// add mapper
var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// add cors
app.UseCors(builder =>
{
    builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
