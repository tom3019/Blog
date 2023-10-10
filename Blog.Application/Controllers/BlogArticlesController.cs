using Blog.Application.Models.BlogArticles.Request;
using Blog.SeedWork;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BlogArticlesController : ControllerBase
{
    private readonly IMediator _mediator;

    public BlogArticlesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateArticleRequest request) => Ok(await _mediator.SendAsync(request));
}