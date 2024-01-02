
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using SimpleWebApi8.Web.Models;

namespace SimpleWebApi8.Web.Services;

public class MyThingService
{
    private readonly HttpClient _client;

    public MyThingService(IHttpClientFactory clientFactory)
    {
        _client = clientFactory.CreateClient("MyThingClient");
    }

    public async Task<MyThing> CreateMyThing(MyThing myThing)
    {
        var response = await _client.PostAsJsonAsync("api/mything", myThing);
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadFromJsonAsync<MyThing>();
        return content ?? throw new Exception("Failed to deserialize response content.");
    }

    public async Task<MyThing> GetMyThing(Guid id)
    {
        var response = await _client.GetAsync($"api/mything/{id}");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadFromJsonAsync<MyThing>();
        return content ?? throw new Exception("Failed to deserialize response content.");
    }
}