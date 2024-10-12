using MigrationTool.Web.Data;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

if(app.Environment.IsDevelopment())
{
    Seeder.Init();
}

app.Run();
