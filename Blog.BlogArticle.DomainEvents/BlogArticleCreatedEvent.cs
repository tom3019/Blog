using Blog.SeedWork;

namespace Blog.BlogArticle.DomainEvents;

/// <summary>
/// 部落格文章建立事件
/// </summary>
public record BlogArticleCreatedEvent : DomainEvent
{
    /// <summary>
    /// 文章Id
    /// </summary>
    public Guid ArticleId { get; set; }

    /// <summary>
    /// 會員Id
    /// </summary>
    public Guid MemberId { get; set; }

    /// <summary>
    /// 建立時間
    /// </summary>
    public DateTime CreateDate { get; set; }
}