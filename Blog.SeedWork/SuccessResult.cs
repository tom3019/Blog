namespace Blog.SeedWork;

public class SuccessResult : IResponse
{
    public bool Success { get; set; }
    public string ErrorMessage { get; set; }
}