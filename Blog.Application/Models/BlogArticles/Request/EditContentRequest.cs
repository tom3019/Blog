using Blog.SeedWork;

namespace Blog.Application.Models.BlogArticles.Request;

public class EditContentRequest:IRequest
{
    public Guid ArticleId { get; set; }
    public string Content { get; set; }
}