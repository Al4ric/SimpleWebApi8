using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Testcontainers.PostgreSql;

namespace SimpleWebApi8.Tests;

public class CustomWebAppFactory<TProgram>
    : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        var postgresContainer = new PostgreSqlBuilder()
            .WithDatabase("test")
            .WithUsername("postgres")
            .WithPassword("SecretPassword")
            .WithPortBinding(5432, true)
            .Build();

        postgresContainer.StartAsync().GetAwaiter().GetResult();
        builder.UseSetting("ConnectionStrings:Postgres", postgresContainer.GetConnectionString());
    }
}