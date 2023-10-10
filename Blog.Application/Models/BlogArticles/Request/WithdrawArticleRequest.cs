using Blog.SeedWork;

namespace Blog.Application.Models.BlogArticles.Request;

public class WithdrawArticleRequest : IRequest
{
    public Guid ArticleId { get; set; }
}