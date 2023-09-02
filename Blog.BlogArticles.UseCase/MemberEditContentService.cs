using Blog.BlogArticles.Entities;
using Blog.BlogArticles.UseCase.Exceptions;
using Blog.BlogArticles.UseCase.Port;
using Blog.BlogArticles.UseCase.Port.In.MemberEditContent;
using Blog.BlogArticles.UseCase.Port.Out;
using Blog.SeedWork;

namespace Blog.BlogArticles.UseCase;

/// <summary>
/// 會員編輯文章內容
/// </summary>
public class MemberEditContentService : IMemberEditContentService
{
    private readonly IDomainEventBus _domainEventBus;
    private readonly ILoadBlogArticlePort _loadBlogArticlePort;
    private readonly ISaveBlogArticlePort _saveBlogArticlePort;

    public MemberEditContentService(IDomainEventBus domainEventBus,
        ILoadBlogArticlePort loadBlogArticlePort,
        ISaveBlogArticlePort saveBlogArticlePort)
    {
        _domainEventBus = domainEventBus;
        _loadBlogArticlePort = loadBlogArticlePort;
        _saveBlogArticlePort = saveBlogArticlePort;
    }

    /// <summary>
    /// 處理程序
    /// </summary>
    /// <param name="import"></param>
    /// <returns></returns>
    public async Task<SuccessResult> HandleAsync(MemberEditContentImport import)
    {
        var blogArticle = await _loadBlogArticlePort.LoadAsync(new BlogArticleId(import.BlogArticleId));
        if (blogArticle.IsNull())
        {
            throw new BlogArticleNotFoundException($"Id : {import.BlogArticleId} not found.");
        }

        blogArticle.ChangeContent(import.Content);

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