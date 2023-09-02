using Blog.BlogArticles.Entities;

namespace Blog.BlogArticles.UseCase.Port.Out;

public interface ILoadBlogArticlePort
{
    /// <summary>
    /// 載入部落格文章
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<BlogArticle> LoadAsync(BlogArticleId id);
}