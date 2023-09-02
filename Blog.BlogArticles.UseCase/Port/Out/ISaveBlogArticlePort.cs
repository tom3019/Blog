using Blog.BlogArticles.Entities;

namespace Blog.BlogArticles.UseCase.Port.Out;

public interface ISaveBlogArticlePort
{
    /// <summary>
    /// 儲存部落格文章
    /// </summary>
    /// <param name="blogArticle"></param>
    /// <returns></returns>
    Task<bool> SaveAsync(BlogArticle blogArticle);
}