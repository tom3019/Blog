using Blog.SeedWork;

namespace Blog.BlogArticles.UseCase.Port.In.ArticlePublish;

/// <summary>
/// 文章上架
/// </summary>
public interface IArticlePublishService
{
    /// <summary>
    /// 處理程序
    /// </summary>
    /// <param name="import"></param>
    /// <returns></returns>
    Task<SuccessResult> HandleAsync(ArticlePublishImport import);
}