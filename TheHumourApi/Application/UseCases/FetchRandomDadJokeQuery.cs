using Application.Services;
using Application.Models.DTOs;
using AutoMapper;
using MediatR;

namespace Application.UseCases;

public class FetchRandomHumourQuery : IRequest<HumourDto> { }

public class FetchRandomHumourQueryHandler : IRequestHandler<FetchRandomHumourQuery, HumourDto>
{
    private readonly IDadJokeClient dadJokeClient;
    private readonly IMapper mapper;
    public FetchRandomHumourQueryHandler(IDadJokeClient dadJokeClient, IMapper mapper)
    {
        this.dadJokeClient = dadJokeClient;
        this.mapper = mapper;
    }
    public async Task<HumourDto> Handle(FetchRandomHumourQuery request, CancellationToken cancellationToken)
    {
        var humour = await dadJokeClient.GetRandomHumour();
        var result = mapper.Map<HumourDto>(humour);
        return result;
    }
}