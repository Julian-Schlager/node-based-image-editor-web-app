using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NodeEditor.BuisnessLogic.Implementation;
using NodeEditor.BuisnessLogic.Interfaces;
using NodeEditor.DataAccess;
using NodeEditor.DataAccess.EfCore;
using System.Configuration;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Business Logic
builder.Services.AddScoped<INodeService, NodeService>();
builder.Services.AddScoped<INodeTypeService, NodeTypeService>();
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

app.UseAuthorization();

app.MapControllers();

app.Run();

void ConfigureServices(IServiceCollection services)
{
    services.AddDbContext<NodeEditorContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("NodeEditorDB"))
);
}