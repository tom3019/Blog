namespace Blog.SeedWork;

/// <summary>
/// 領域事件
/// </summary>
public abstract record DomainEvent
{
    /// <summary>
    /// 建立時間
    /// </summary>
    public DateTime CreateAt { get; set; } = DateTime.Now;

    /// <summary>
    /// EventId
    /// </summary>
    public Guid EventId { get; set; } = Guid.NewGuid();
}