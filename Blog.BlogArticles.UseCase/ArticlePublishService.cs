using Blog.BlogArticles.Entities;
using Blog.BlogArticles.UseCase.Exceptions;
using Blog.BlogArticles.UseCase.Port.In.ArticlePublish;
using Blog.BlogArticles.UseCase.Port.Out;
using Blog.SeedWork;

namespace Blog.BlogArticles.UseCase;

/// <summary>
/// 文章上架
/// </summary>
public class ArticlePublishService : IArticlePublishService
{
    private readonly ILoadBlogArticlePort _loadBlogArticlePort;
    private readonly IDomainEventBus _domainEventBus;
    private readonly ISaveBlogArticlePort _saveBlogArticlePort;

    public ArticlePublishService(ILoadBlogArticlePort loadBlogArticlePort,
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
    public async Task<SuccessResult> HandleAsync(ArticlePublishImport import)
    {
        var blogArticle = await _loadBlogArticlePort.LoadAsync(new BlogArticleId(import.BlogArticleId));
        if (blogArticle.IsNull())
        {
            throw new BlogArticleNotFoundException($"Id : {import.BlogArticleId} not found.");
        }

        blogArticle.Publish();

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