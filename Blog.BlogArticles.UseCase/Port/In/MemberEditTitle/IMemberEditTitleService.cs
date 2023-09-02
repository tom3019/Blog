using Blog.SeedWork;

namespace Blog.BlogArticles.UseCase.Port.In.MemberEditTitle;

/// <summary>
/// 會員編輯文章標題
/// </summary>
public interface IMemberEditTitleService
{
    /// <summary>
    /// 處理程序
    /// </summary>
    /// <param name="import"></param>
    /// <returns></returns>
    Task<SuccessResult> HandleAsync(MemberEditTitleImport import);
}