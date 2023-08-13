using Blog.SeedWork;

namespace Blog.BlogArticle.DomainEvents;

/// <summary>
/// 部落格文章上架事件
/// </summary>
public record BlogArticlePublishedEvent : DomainEvent
{
    /// <summary>
    /// 文章Id
    /// </summary>
    public Guid ArticleId { get; set; }
}