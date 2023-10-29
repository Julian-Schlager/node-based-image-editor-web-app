using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NodeEditor.BuisnessLogic.Implementation;
using NodeEditor.BuisnessLogic.Interfaces;
using NodeEditor.DataAccess;
using NodeEditor.DataAccess.EfCore;
using System.Configuration;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

string[] allowedHosts = builder.Configuration["AllowedOrigins"].Split(';');



builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins(allowedHosts)
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Business Logic
builder.Services.AddScoped<INodeService, NodeService>();
builder.Services.AddScoped<INodeTypeService, NodeTypeService>();
builder.Services.AddScoped<IImageService, ImageService>();
//Repositories
builder.Services.AddScoped<INodeRepository, NodeRepository>();
builder.Services.AddScoped<INodeTypeRepository, NodeTypeRepository>();

builder.Services.AddDbContext<NodeEditorContext>(options =>
       options.UseSqlServer(builder.Configuration.GetConnectionString("NodeEditorDB")));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();

//void ConfigureServices(IServiceCollection services)
//{
//    services.AddDbContext<NodeEditorContext>(options =>
//        options.UseSqlServer(builder.Configuration.GetConnectionString("NodeEditorDB"))
//);
//}