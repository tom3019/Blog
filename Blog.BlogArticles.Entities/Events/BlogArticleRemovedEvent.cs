using Blog.SeedWork;

namespace Blog.BlogArticles.Entities.Events;

/// <summary>
/// 部落格文章移除事件
/// </summary>
public record BlogArticleRemovedEvent: DomainEvent
{
    /// <summary>
    /// 文章Id
    /// </summary>
    public Guid ArticleId { get; set; }
}