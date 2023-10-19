using System.Net;
using Application.Models.DTOs;
using Application.UseCases;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

/// <summary>
/// Provides endpoints to fetch humour.
/// </summary>
[ApiController]
[Route("api/humour")]
public class HumourController : ControllerBase
{
    private readonly IMediator mediator;
    /// <summary>
    /// Constructor to initialize field members.
    /// </summary>
    /// <param name="mediator">Mediator</param>
    public HumourController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    /// <summary>
    /// Fetch humour for a given humour id.
    /// </summary>
    /// <param name="id">humour identifier</param>
    /// <returns>humour details</returns>
    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
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

    /// <summary>
    /// Fetch any random humour.
    /// </summary>
    /// <returns>humour details</returns>
    [HttpGet]
    [Route("random")]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<HumourDto> GetRandomHumour()
    {
        var humour = await mediator.Send(new FetchRandomHumourQuery());
        return humour;
    }

    /// <summary>
    /// Search for homours having a <paramref name="term"/> in it.
    /// </summary>
    /// <param name="page">page number.</param>
    /// <param name="limit">number of humours per page.</param>
    /// <param name="term">search text in the humour.</param>
    /// <returns>list of matching homours</returns>
    [HttpGet]
    [Route("search")]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> Search([FromQuery] int page = 1, [FromQuery] int limit = 20, [FromQuery] string term = null)
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
