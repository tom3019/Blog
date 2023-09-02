using Blog.BlogArticles.Entities;
using Blog.BlogArticles.UseCase.Port.In.MemberEditTitle;
using Blog.BlogArticles.UseCase.Port.Out;
using Blog.SeedWork;

namespace Blog.BlogArticles.UseCase;

/// <summary>
/// 會員編輯文章標題
/// </summary>
public class MemberEditTitleService : IMemberEditTitleService
{
    private readonly ILoadBlogArticlePort _loadBlogArticlePort;
    private readonly IDomainEventBus _domainEventBus;
    private readonly ISaveBlogArticlePort _saveBlogArticlePort;

    public MemberEditTitleService(ILoadBlogArticlePort loadBlogArticlePort,
        IDomainEventBus domainEventBus,
        ISaveBlogArticlePort saveBlogArticlePort)
    {
        _loadBlogArticlePort = loadBlogArticlePort;
        _domainEventBus = domainEventBus;
        _saveBlogArticlePort = saveBlogArticlePort;
    }

    /// <summary>
    /// 處理程序
    /// </summary>
    /// <param name="import"></param>
    /// <returns></returns>
    public async Task<SuccessResult> HandleAsync(MemberEditTitleImport import)
    {
        var blogArticle = await _loadBlogArticlePort.LoadAsync(new BlogArticleId(import.BlogArticleId));
        blogArticle.ChangeTitle(import.Title);

        var success = await _saveBlogArticlePort.SaveAsync(blogArticle);
        if (!success)
        {
            return new SuccessResult
            {
                Success = success,
                ErrorMessage = "Save blog article fail."
            };
        }

        await _domainEventBus.DispatchEventPublishAsync(blogArticle);
        return new SuccessResult
        {
            Success = success,
        };
    }
}