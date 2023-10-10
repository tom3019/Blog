using Blog.SeedWork;

namespace Blog.Application.Models.BlogArticles.Request;

public class PublishArticleRequest:IRequest
{
    public Guid ArticleId { get; set; }
}