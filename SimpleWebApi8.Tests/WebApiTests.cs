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
    public async Task TestCreateMyThingEndpoint()
    {
        // Arrange
        var httpRequest = new HttpRequestMessage(HttpMethod.Get, "/createMyThing");

        // Act
        var httpResponse = await _client.SendAsync(httpRequest);

        // Assert
        httpResponse.EnsureSuccessStatusCode(); // fails the test if the status code is not a success status code (2xx)
        var responseContent = await httpResponse.Content.ReadAsStringAsync();
        var myThing = JsonConvert.DeserializeObject<MyThing>(responseContent);

        Assert.Equal("My Thing", myThing?.Name);
    }

    [Fact]
public async Task TestCreateAndRetrieveMyThingEndpoint()
{
    // Arrange
    var createRequest = new HttpRequestMessage(HttpMethod.Get, $"/createMyThing");

    // Act
    var createResponse = await _client.SendAsync(createRequest);

    // Assert
    createResponse.EnsureSuccessStatusCode(); // fails the test if the status code is not a success status code (2xx)
    var createResponseContent = await createResponse.Content.ReadAsStringAsync();
    var createdMyThing = JsonConvert.DeserializeObject<MyThing>(createResponseContent);
    
    var myThingId = createdMyThing?.Id ?? Guid.Empty;
    var myThing = new MyThing("My Thing", myThingId)
    {
        Description = "This is my thing",
        Tags = ["tag1", "tag2"],
        Picture = [], // replace with your picture bytes
        Place = "My place"
    };

    Assert.Equal(myThing.Name, createdMyThing?.Name);
    Assert.Equal(myThing.Description, createdMyThing?.Description);
    Assert.Equal(myThing.Tags, createdMyThing?.Tags);
    Assert.Equal(myThing.Picture, createdMyThing?.Picture);
    Assert.Equal(myThing.Place, createdMyThing?.Place);

    // Arrange
    var retrieveRequest = new HttpRequestMessage(HttpMethod.Get, $"/myThing/{myThingId}");

    // Act
    var retrieveResponse = await _client.SendAsync(retrieveRequest);

    // Assert
    retrieveResponse.EnsureSuccessStatusCode(); // fails the test if the status code is not a success status code (2xx)
    var retrieveResponseContent = await retrieveResponse.Content.ReadAsStringAsync();
    var retrievedMyThing = JsonConvert.DeserializeObject<MyThing>(retrieveResponseContent);

    Assert.Equal(myThing.Name, retrievedMyThing?.Name);
    Assert.Equal(myThing.Description, retrievedMyThing?.Description);
    Assert.Equal(myThing.Tags, retrievedMyThing?.Tags);
    Assert.Equal(myThing.Picture, retrievedMyThing?.Picture);
    Assert.Equal(myThing.Place, retrievedMyThing?.Place);
}
}