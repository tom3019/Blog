using Blog.BlogArticles.Entities.Exceptions;
using Blog.SeedWork;

namespace Blog.BlogArticles.Entities;

public record MemberId:ValueObject<MemberId>
{
    public Guid Value { get; private set; }

    public MemberId(Guid value)
    {
        if (value == default)
        {
            throw new BlogArticleDomainException( "MemberId cannot be default(Guid).");
        }

        Value = value;
    }
    public static implicit operator Guid(MemberId self) => self.Value;
    public static implicit operator MemberId(string value) => new(Guid.Parse(value));

    public override string ToString() => Value.ToString();

    protected MemberId() { }
}