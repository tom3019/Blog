using Blog.SeedWork;

namespace Blog.BlogArticles.Entities.Events;

/// <summary>
/// 部落格文章內容變更事件
/// </summary>
public record BlogArticleContentChangedEvent:DomainEvent
{
    /// <summary>
    /// 文章Id
    /// </summary>
    public Guid ArticleId { get; set; }

    /// <summary>
    /// 內容
    /// </summary>
    public string Content { get; set; }
}