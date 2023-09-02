using Blog.SeedWork;

namespace Blog.BlogArticles.UseCase.Port.In.ArticleWithdraw;

public interface IArticleWithdrawService
{
    /// <summary>
    /// 處理程序
    /// </summary>
    /// <param name="import"></param>
    /// <returns></returns>
    Task<SuccessResult> HandleAsync(ArticleWithdrawImport import);
}