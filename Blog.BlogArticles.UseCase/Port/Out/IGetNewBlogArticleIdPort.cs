namespace Blog.BlogArticles.UseCase.Port.Out;

public interface IGetNewBlogArticleIdPort
{
    /// <summary>
    /// 取的新的部落格文章Id
    /// </summary>
    /// <returns></returns>
    Task<Guid> GetNewBlogArticleIdAsync();
}