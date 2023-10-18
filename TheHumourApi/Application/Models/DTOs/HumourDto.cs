using System.Text.Json.Serialization;

namespace Application.Models.DTOs;

public class HumourDto
{
    [JsonPropertyName("id")]
    public string Id {get;set;}
    
    [JsonPropertyName("text")]
    public string Text {get;set;}
}