namespace Blog.SeedWork;

/// <summary>
/// 使用情境
/// </summary>
/// <typeparam name="TInput"></typeparam>
/// <typeparam name="TOutput"></typeparam>
public interface IUseCase<in TInput,TOutput>
{
    /// <summary>
    /// 處理程序
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<TOutput> HandleAsync(TInput input);
}