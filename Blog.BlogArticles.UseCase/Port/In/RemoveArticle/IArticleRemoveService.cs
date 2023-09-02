using Blog.SeedWork;

namespace Blog.BlogArticles.UseCase.Port.In.RemoveArticle;

public interface IArticleRemoveService
{
    /// <summary>
    /// 處理程序
    /// </summary>
    /// <param name="import"></param>
    /// <returns></returns>
    Task<SuccessResult> HandleAsync(ArticleRemoveImport import);
}