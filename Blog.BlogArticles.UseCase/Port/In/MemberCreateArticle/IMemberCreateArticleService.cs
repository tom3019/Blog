using Blog.SeedWork;

namespace Blog.BlogArticles.UseCase.Port.In.MemberCreateArticle;

public interface IMemberCreateArticleService
{
    /// <summary>
    /// 處理程序
    /// </summary>
    /// <param name="import"></param>
    /// <returns></returns>
    Task<SuccessResult> HandleAsync(MemberCreateArticleImport import);
}