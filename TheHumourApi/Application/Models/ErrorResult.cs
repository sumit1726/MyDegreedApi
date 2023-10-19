namespace Application.Models;

public class ErrorResult
{
    public List<string> ErrorMessages {get;set;} = new();
    public string Source{get;set;}
    public string Exception{get;set;}
    public string ErrorId {get;set;}
    public int StatusCode {get;set;}
}