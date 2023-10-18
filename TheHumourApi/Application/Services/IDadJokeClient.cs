using Application.Core.Models;

namespace Application.Services;

public interface IDadJokeClient
{
    Task<Humour> GetRandomHumour();
    Task<Humour> GetHumourById(string humourId);
    IAsyncEnumerable<PagedHumour> Search(int page, int pageLimit, string searchTerm);
}