using Newtonsoft.Json;

namespace SimpleWebApi8.Tests;

public class WebAppTest : IClassFixture<CustomWebAppFactory<Program>>
{
    private readonly HttpClient _client;

    public WebAppTest(CustomWebAppFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task TestCreateDeviceEndpoint()
    {
        // Arrange
        var httpRequest = new HttpRequestMessage(HttpMethod.Get, "/createDevice");

        // Act
        var httpResponse = await _client.SendAsync(httpRequest);

        // Assert
        httpResponse.EnsureSuccessStatusCode(); // fails the test if the status code is not a success status code (2xx)
        var responseContent = await httpResponse.Content.ReadAsStringAsync();
        var device = JsonConvert.DeserializeObject<Device>(responseContent);

        Assert.Equal("My Device", device?.Name);
    }
}