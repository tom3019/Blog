using Blog.BlogArticle.Entities.Exceptions;
using Blog.SeedWork;

namespace Blog.BlogArticle.Entities;

/// <summary>
/// 文章內容
/// </summary>
public class ArticleContent:ValueObject<ArticleContent>
{
    public string Value { get; }
    internal ArticleContent(string value) => Value = value;

    public static ArticleContent FromString(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new BlogArticleDomainException("文章內容不可為空");
        }

        return new ArticleContent(value);
    }

    public static implicit operator string(ArticleContent self) => self.Value;

}