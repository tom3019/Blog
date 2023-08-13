namespace Blog.SeedWork;

/// <summary>
/// 領域事件
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IDomainEvent<T>
{
    /// <summary>
    /// 建立時間
    /// </summary>
    public DateTime CreateAt { get; set; }

    /// <summary>
    /// Entity
    /// </summary>
    public T Entity { get; set; }
}