using Blog.BlogArticles.Entities;
using Blog.BlogArticles.UseCase.Exceptions;
using Blog.BlogArticles.UseCase.Port.In.ArticleWithdraw;
using Blog.BlogArticles.UseCase.Port.Out;
using Blog.SeedWork;

namespace Blog.BlogArticles.UseCase;

/// <summary>
/// 文章撤銷
/// </summary>
public class ArticleWithdrawService : IArticleWithdrawService
{
    private readonly IDomainEventBus _domainEventBus;
    private readonly ILoadBlogArticlePort _loadBlogArticlePort;
    private readonly ISaveBlogArticlePort _saveBlogArticlePort;

    public ArticleWithdrawService(IDomainEventBus domainEventBus,
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
    public async Task<SuccessResult> HandleAsync(ArticleWithdrawImport import)
    {
        var blogArticle = await _loadBlogArticlePort.LoadAsync(new BlogArticleId(import.BlogArticleId));
        if (blogArticle.IsNull())
        {
            throw new BlogArticleNotFoundException($"Id : {import.BlogArticleId} not found.");
        }

        blogArticle.Withdraw();

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