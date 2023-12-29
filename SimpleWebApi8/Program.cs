using Microsoft.OpenApi.Models;
using Marten;
using Marten.Events.Projections;
using SimpleWebApi8;
using Weasel.Core;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "Event Sourcing API", Version = "v1" }); });
var connectionString = builder.Configuration.GetConnectionString("Postgres");
builder.Services.AddMarten(options =>
    {
        if (connectionString != null) options.Connection(connectionString);
        options.AutoCreateSchemaObjects = AutoCreate.All;
    })
    .ApplyAllDatabaseChangesOnStartup();
builder.Services.AddScoped(GetDeviceStore);
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); });

app.MapGet("/", () => "Hello World!");
app.MapGet("/random", () => new Random().Next(1, 100).ToString());
app.MapGet("createMyThing", ThingHandlers.CreateMyThing);
app.MapGet("myThing/{myThingId:guid}", ThingHandlers.GetMyThingById);
app.Run();
return;

DocumentStore GetDeviceStore(IServiceProvider serviceProvider)
{
    var documentStore = DocumentStore.For(options =>
    {
        if (connectionString != null) options.Connection(connectionString);
        options.Projections.Snapshot<MyThing>(SnapshotLifecycle.Inline);
    });
    return documentStore;
}

public partial class Program
{
}