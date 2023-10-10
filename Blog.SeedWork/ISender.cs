namespace Blog.SeedWork;

public interface ISender
{
    Task<IResponse> SendAsync(IRequest request);
}