using LoginFormForGameInUnity.Attributes;
using LoginFormForGameInUnity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.Parse("8.0.31-mysql"));
});



#region swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "My Sample FormLogin WebAPI",
        Description = "My Sample FormLogin WebAPI"
    });

    c.AddSecurityDefinition("Basic", new OpenApiSecurityScheme { In = ParameterLocation.Header, Description = "Please add Basic Token", Name = "Authorization", Type = SecuritySchemeType.ApiKey, Scheme = "Basic" });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                 {
                   {
                        new OpenApiSecurityScheme
                        {
                       Reference = new OpenApiReference {
                         Type = ReferenceType.SecurityScheme,
                         Id = "Basic"
                     }
                     },
                   new string[] { }
                  }
                });

    var basePath = Directory.GetCurrentDirectory();
    var xmlPath = Path.Combine(basePath, "WebAPI.xml");
    c.IncludeXmlComments(xmlPath);
    c.DocumentFilter<HiddenApiFilter>();
    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
});
#endregion



var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}


app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
