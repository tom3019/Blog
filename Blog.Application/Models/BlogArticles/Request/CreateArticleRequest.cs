using Blog.SeedWork;

namespace Blog.Application.Models.BlogArticles.Request;

public class CreateArticleRequest : IRequest
{
    public Guid MemberId { get; set; }
}