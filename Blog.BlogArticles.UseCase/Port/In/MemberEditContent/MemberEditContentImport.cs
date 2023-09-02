namespace Blog.BlogArticles.UseCase.Port.In.MemberEditContent;

public class MemberEditContentImport
{
    /// <summary>
    /// 文章Id
    /// </summary>
    public Guid BlogArticleId { get; set; }
    
    /// <summary>
    /// 內容
    /// </summary>
    public string Content { get; set; }
}