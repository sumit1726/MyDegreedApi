using System.Text.Json;
using Application.Core.Models;
using Application.Services;

namespace Infrastructure.Services;

public class DadJokeClient : IDadJokeClient
{
    private readonly HttpClient httpClient;

    public DadJokeClient(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }
    public async Task<Humour> GetHumourById(string humourId)
    {
        string url = $"j/{humourId}";
        var httpResponseMessage = await httpClient.GetAsync(url);
        httpResponseMessage.EnsureSuccessStatusCode();
        string responseString = await httpResponseMessage.Content.ReadAsStringAsync();
        var humour = JsonSerializer.Deserialize<Humour>(responseString);
        return humour;
    }

    public async Task<Humour> GetRandomHumour()
    {
        string url = $"";
        var httpResponseMessage = await httpClient.GetAsync(url);
        httpResponseMessage.EnsureSuccessStatusCode();
        string responseString = await httpResponseMessage.Content.ReadAsStringAsync();
        var humour = JsonSerializer.Deserialize<Humour>(responseString);
        return humour;
    }

    private async IAsyncEnumerable<T> SearchInternal<T>(int page, int pageLimit, string searchTerm)
    {
        string url= $"search?page={page}&limit={pageLimit}&term={searchTerm}";
        var httpResponseMessage = await httpClient.GetAsync(url);
        httpResponseMessage.EnsureSuccessStatusCode();
        string responseString = await httpResponseMessage.Content.ReadAsStringAsync();
        var data = JsonSerializer.Deserialize<T>(responseString);
        yield return data;
    }
    public async IAsyncEnumerable<PagedHumour> Search(int page, int pageLimit, string searchTerm)
    {
        await foreach (var pagedResult in SearchInternal<PagedHumour>(page, pageLimit, searchTerm))
        {
            yield return pagedResult;
        }
    }
}