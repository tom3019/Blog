namespace Blog.SeedWork;

/// <summary>
/// 內部事件處理程序
/// </summary>
public interface IInternalEventHandler
{
    /// <summary>
    /// 事件處理程序
    /// </summary>
    /// <param name="event"></param>
    void Handle(object @event);
}