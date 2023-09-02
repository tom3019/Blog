using Blog.SeedWork;

namespace Blog.BlogArticles.DomainEvents;

/// <summary>
/// 部落格文章標題變更事件
/// </summary>
public record BlogArticleTitleChangedEvent : DomainEvent
{
    /// <summary>
    /// 文章Id
    /// </summary>
    public Guid ArticleId { get; set; }

    /// <summary>
    /// Title
    /// </summary>
    public string Title { get; set; }
}