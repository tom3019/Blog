using Blog.SeedWork;

namespace Blog.BlogArticle.Entities;

/// <summary>
/// 文章標題
/// </summary>
public class ArticleTitle : ValueObject<ArticleTitle>
{
    public string Value { get; }
    
    public ArticleTitle(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new BlogArticleDomainException("文章標題不可為空");
        }

        if (value.Length > 100)
        {
            throw new BlogArticleDomainException("文章標題不可超過100個字");
        }

        Value = value;
    }
    
    public static implicit operator string(ArticleTitle self) => self.Value;
    
    public override string ToString() => Value;
    
    protected ArticleTitle() { }
}