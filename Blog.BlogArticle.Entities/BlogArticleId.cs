using Blog.SeedWork;

namespace Blog.BlogArticle.Entities;

public class BlogArticleId:ValueObject<BlogArticleId>
{
    public Guid Value { get; }

    public BlogArticleId(Guid value)
    {
        if (value == default)
        {
            throw new BlogArticleDomainException( "BlogArticleId cannot be default(Guid).");
        }

        Value = value;
    }
    public static implicit operator Guid(BlogArticleId self) => self.Value;
    public static implicit operator BlogArticleId(string value) => new(Guid.Parse(value));

    public override string ToString() => Value.ToString();

    protected BlogArticleId() { }
}