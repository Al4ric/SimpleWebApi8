using Marten;
using Marten.Events.Projections;
using SimpleWebApi8;
using Weasel.Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("Postgres");

builder.Services.AddMarten(options =>
    {
        if (connectionString != null) options.Connection(connectionString);
        options.AutoCreateSchemaObjects = AutoCreate.All;
    })
    .ApplyAllDatabaseChangesOnStartup();

builder.Services.AddScoped(GetDeviceStore);

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
// add endpoint that returns a random number
app.MapGet("/random", () => new Random().Next(1, 100).ToString());

app.MapGet("createDevice", DeviceHandlers.CreateDevice);

app.MapGet("device/{deviceId:guid}", DeviceHandlers.GetDeviceById);

app.Run();
return;

DocumentStore GetDeviceStore(IServiceProvider serviceProvider)
{
    var documentStore = DocumentStore.For(options =>
    {
        if (connectionString != null) options.Connection(connectionString);
        options.Projections.Snapshot<Device>(SnapshotLifecycle.Inline);
    });
    return documentStore;
}

public partial class Program { }