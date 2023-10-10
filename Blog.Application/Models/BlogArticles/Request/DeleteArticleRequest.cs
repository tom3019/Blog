using Blog.SeedWork;

namespace Blog.Application.Models.BlogArticles.Request;

public class DeleteArticleRequest:IRequest
{
    public Guid ArticleId { get; set; }
}