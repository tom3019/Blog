using Blog.SeedWork;

namespace Blog.BlogArticle.DomainEvents;

/// <summary>
/// 部落格文章下架事件
/// </summary>
public record BlogArticleWithdrawnEvent : DomainEvent
{
    /// <summary>
    /// 文章Id
    /// </summary>
    public Guid ArticleId { get; set; }
}