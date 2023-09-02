using Blog.SeedWork;

namespace Blog.BlogArticles.UseCase.Port.In.MemberEditContent;

/// <summary>
/// 會員編輯文章內容
/// </summary>
public interface IMemberEditContentService
{
    /// <summary>
    /// 處理程序
    /// </summary>
    /// <param name="import"></param>
    /// <returns></returns>
    Task<SuccessResult> HandleAsync(MemberEditContentImport import);
}