using System.Text.Json.Serialization;
using Application.Services;
using Application.Models.DTOs;
using AutoMapper;
using MediatR;
using FluentValidation;

namespace Application.UseCases;

public class SearchDadJokesQueryValidator : AbstractValidator<SearchDadJokesQuery>
{
    public SearchDadJokesQueryValidator()
    {
        RuleFor(q => q.PageNumber).NotNull().GreaterThanOrEqualTo(1).WithMessage("Page should be numeric and greater than or equals to 1");
        RuleFor(q => q.PageSizeLimit).NotNull().GreaterThanOrEqualTo(1).LessThanOrEqualTo(30).WithMessage("Page should be numeric and within range from 1 to 30.");
        RuleFor(q => q.SearchTerm).NotNull().NotEmpty().WithMessage("Term cannot be null or empty.");
    }
}
public class SearchDadJokesQuery : IRequest<PagedHumourDto>
{
    [JsonPropertyName("page")]
    public int PageNumber {get;set;}
    [JsonPropertyName("limit")]
    public int PageSizeLimit {get;set;}
    [JsonPropertyName("term")]
    public string SearchTerm {get;set;}
}

public class SearchDadJokesQueryHandler : IRequestHandler<SearchDadJokesQuery, PagedHumourDto>
{
    private readonly IDadJokeClient dadJokeClient;
    private readonly IMapper mapper;
    public SearchDadJokesQueryHandler(IDadJokeClient dadJokeClient, IMapper mapper)
    {
        this.dadJokeClient = dadJokeClient;
        this.mapper = mapper;
    }
    public async Task<PagedHumourDto> Handle(SearchDadJokesQuery request, CancellationToken cancellationToken)
    {
        var response = dadJokeClient.Search(request.PageNumber, request.PageSizeLimit, request.SearchTerm);
        PagedHumourDto? humours  = null;
        await foreach (var pagedResult in response)
        {
            humours = mapper.Map<PagedHumourDto>(pagedResult);
        }
        return humours;
    }
}