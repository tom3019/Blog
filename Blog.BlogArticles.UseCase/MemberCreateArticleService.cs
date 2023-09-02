using Blog.BlogArticles.Entities;
using Blog.BlogArticles.UseCase.Port.In.MemberCreateArticle;
using Blog.BlogArticles.UseCase.Port.Out;
using Blog.SeedWork;

namespace Blog.BlogArticles.UseCase;

/// <summary>
/// 會員建立文章
/// </summary>
public class MemberCreateArticleService : IMemberCreateArticleService
{
    private readonly IDomainEventBus _domainEventBus;
    private readonly ISaveBlogArticlePort _saveBlogArticlePort;
    private readonly IGetNewBlogArticleIdPort _getNewBlogArticleIdPort;

    public MemberCreateArticleService(IDomainEventBus domainEventBus,
        ISaveBlogArticlePort saveBlogArticlePort,
        IGetNewBlogArticleIdPort getNewBlogArticleIdPort)
    {
        _domainEventBus = domainEventBus;
        _saveBlogArticlePort = saveBlogArticlePort;
        _getNewBlogArticleIdPort = getNewBlogArticleIdPort;
    }

    /// <summary>
    /// 處理程序
    /// </summary>
    /// <param name="import"></param>
    /// <returns></returns>
    public async Task<SuccessResult> HandleAsync(MemberCreateArticleImport import)
    {
        var blogArticleId = await _getNewBlogArticleIdPort.GetNewBlogArticleIdAsync();
        var blogArticle = new BlogArticle(new BlogArticleId(blogArticleId), new MemberId(import.MemberId));
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