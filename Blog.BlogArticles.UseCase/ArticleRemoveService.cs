using Blog.BlogArticles.Entities;
using Blog.BlogArticles.UseCase.Port.In.RemoveArticle;
using Blog.BlogArticles.UseCase.Port.Out;
using Blog.SeedWork;

namespace Blog.BlogArticles.UseCase;

/// <summary>
/// 文章刪除
/// </summary>
public class ArticleRemoveService : IArticleRemoveService
{
    private readonly ILoadBlogArticlePort _loadBlogArticlePort;
    private readonly IDomainEventBus _domainEventBus;
    private readonly ISaveBlogArticlePort _saveBlogArticlePort;

    public ArticleRemoveService(ILoadBlogArticlePort loadBlogArticlePort,
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
    public async Task<SuccessResult> HandleAsync(ArticleRemoveImport import)
    {
        var blogArticle = await _loadBlogArticlePort.LoadAsync(new BlogArticleId(import.BlogArticleId));
        blogArticle.Remove();

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