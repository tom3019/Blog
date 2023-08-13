using Blog.SeedWork;

namespace Blog.BlogArticle.DomainEvents;

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