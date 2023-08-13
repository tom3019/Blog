namespace Blog.BlogArticle.Entities;

/// <summary>
/// 部落格文章領域例外
/// </summary>
public class BlogArticleDomainException : Exception
{
    public BlogArticleDomainException(string message):base(message)
    {

    }
}