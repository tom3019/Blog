namespace Blog.BlogArticles.UseCase.Port.In.MemberEditTitle;

public class MemberEditTitleImport
{
    /// <summary>
    /// 文章編號
    /// </summary>
    public Guid BlogArticleId { get; set; }
    
    /// <summary>
    /// 標題
    /// </summary>
    public string Title { get; set; }
}