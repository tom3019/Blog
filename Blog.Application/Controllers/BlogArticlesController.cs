using Blog.Application.Models.BlogArticles.Request;
using Blog.SeedWork;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BlogArticlesController : ControllerBase
{
    private readonly IMediator _mediator;
    public BlogArticlesController(IMediator mediator) => _mediator = mediator;

    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateArticleRequest request)
        => Ok(await _mediator.SendAsync(request));

    [HttpPatch("Title")]
    public async Task<IActionResult> EditTitleAsync(EditTitleRequest request)
        => Ok(await _mediator.SendAsync(request));

    [HttpPatch("Content")]
    public async Task<IActionResult> EditContentAsync(EditContentRequest request)
        => Ok(await _mediator.SendAsync(request));

    [HttpPatch("Publish")]
    public async Task<IActionResult> PublishAsync(PublishArticleRequest request)
        => Ok(await _mediator.SendAsync(request));

    [HttpPatch("Withdraw")]
    public async Task<IActionResult> WithdrawAsync(WithdrawArticleRequest request)
        => Ok(await _mediator.SendAsync(request));

    [HttpDelete]
    public async Task<IActionResult> DeleteAsync(DeleteArticleRequest request)
        => Ok(await _mediator.SendAsync(request));
}