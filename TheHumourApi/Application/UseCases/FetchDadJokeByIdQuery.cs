using System.Text.Json.Serialization;
using Application.Services;
using Application.Models.DTOs;
using AutoMapper;
using MediatR;
using FluentValidation;

namespace Application.UseCases;

public class FetchHumourByIdQueryValidator : AbstractValidator<FetchHumourByIdQuery>
{
    public FetchHumourByIdQueryValidator()
    {
        RuleFor(q => q.Id).NotNull().NotEmpty().WithMessage("Joke Id cannot be null or empty.");
    }
}
public class FetchHumourByIdQuery : IRequest<HumourDto>
{
    [JsonPropertyName("id")]
    public required string Id {get;set;}
}

public class FetchHumourByIdQueryHandler : IRequestHandler<FetchHumourByIdQuery, HumourDto>
{
    private readonly IDadJokeClient dadJokeClient;
    private readonly IMapper mapper;
    public FetchHumourByIdQueryHandler(IDadJokeClient dadJokeClient, IMapper mapper)
    {
        this.dadJokeClient = dadJokeClient;
        this.mapper = mapper;
    }
    public async Task<HumourDto> Handle(FetchHumourByIdQuery request, CancellationToken cancellationToken)
    {
        var humour = await dadJokeClient.GetHumourById(request.Id);
        var result = mapper.Map<HumourDto>(humour);
        return result;
    }
}