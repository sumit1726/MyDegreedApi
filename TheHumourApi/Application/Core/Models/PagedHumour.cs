using System.Text.Json.Serialization;

namespace Application.Core.Models;

public class PagedHumour
{
    [JsonPropertyName("current_page")]
    public int CurrentPage {get;set;}

    [JsonPropertyName("limit")]
    public int PageLimit {get;set;}

    [JsonPropertyName("next_page")]
    public int NextPage {get;set;}

    [JsonPropertyName("previous_page")]
    public int PreviousPage {get;set;}

    [JsonPropertyName("results")]
    public IList<Humour> Humours {get;set;}

    [JsonPropertyName("term")]
    public string SearchTerm {get;set;}

    [JsonPropertyName("status")]
    public int Status {get;set;}

    [JsonPropertyName("total_jokes")]
    public int TotalJokes {get;set;}

    [JsonPropertyName("total_pages")]
    public int TotalPages {get;set;}
}