using System.Text.Json.Serialization;

namespace Application.Core.Models;

public class Humour
{
    [JsonPropertyName("id")]
    public string HumourId {get;set;}

    [JsonPropertyName("joke")]
    public string HumourText {get;set;}

    [JsonPropertyName("status")]
    public int Status {get;set;}
}