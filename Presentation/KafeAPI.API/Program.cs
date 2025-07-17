using FluentValidation;
using KafeAPI.Application.Dtos.CategoryDtos;
using KafeAPI.Application.Dtos.MenuItemDtos;
using KafeAPI.Application.Interfaces;
using KafeAPI.Application.Mapping;
using KafeAPI.Application.Services.Abstract;
using KafeAPI.Application.Services.Concrete;
using KafeAPI.Persistence.Context;
using KafeAPI.Persistence.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>
    (options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<ITableRepository, TableRepository>();
builder.Services.AddScoped<IMenuItemServices, MenuItemServices>();
builder.Services.AddScoped<ICategoryServices, CategoryServices>();
builder.Services.AddScoped<ITableServices, TableServices>();

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<GeneralMapping>();
});
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddValidatorsFromAssemblyContaining<CreateCategoryDto>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateCategoryDto>();

builder.Services.AddValidatorsFromAssemblyContaining<CreateMenuItemDto>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateMenuItemDto>();

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
var app = builder.Build();

//app.MapScalarApiReference(
//    opt =>
//    {
//        opt.Title = "Kafe API v1";
//        opt.Theme = ScalarTheme.BluePlanet;
//        opt.DefaultHttpClient = new(ScalarTarget.Http, ScalarClient.Http11);
//    });

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", () => Results.Redirect("/swagger"));

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
