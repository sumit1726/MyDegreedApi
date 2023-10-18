using Application.Models.DTOs;
using Application.UseCases;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("humour")]
public class HumourController : ControllerBase
{
    private readonly IMediator mediator;
    public HumourController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetHumourById([FromRoute] string id)
    {
        var request = new FetchHumourByIdQuery() { Id = id };
        var validator = new FetchHumourByIdQueryValidator();
        var validationResult = validator.Validate(request);
        if(!validationResult.IsValid) 
        {
            return BadRequest(validationResult.Errors);
        }
        var humour = await mediator.Send(request);
        return Ok(humour);
    }

    [HttpGet]
    [Route("random")]
    public async Task<HumourDto> GetRandomHumour()
    {
        var humour = await mediator.Send(new FetchRandomHumourQuery());
        return humour;
    }

    [HttpGet]
    [Route("search")]
    public async Task<IActionResult> Search([FromQuery] int page, [FromQuery] int limit, [FromQuery] string term)
    {
        var request = new SearchDadJokesQuery()
        {
            PageNumber = page,
            PageSizeLimit = limit,
            SearchTerm = term
        };
        var validator = new SearchDadJokesQueryValidator();
        var validationResult = validator.Validate(request);
        if(!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }
        var humours = await mediator.Send(request);

        return Ok(humours);
    }
}
