namespace Blog.BlogArticles.UseCase.Exceptions;

/// <summary>
/// 部落格文章不存在Exception
/// </summary>
public class BlogArticleNotFoundException: Exception
{
    public BlogArticleNotFoundException(string message) : base(message)
    {
    }
}