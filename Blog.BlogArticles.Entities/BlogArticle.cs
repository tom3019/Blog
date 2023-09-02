using Blog.BlogArticles.DomainEvents;
using Blog.BlogArticles.Entities.Exceptions;
using Blog.SeedWork;

namespace Blog.BlogArticles.Entities;

/// <summary>
/// 部落格文章
/// </summary>
public class BlogArticle : AggregateRoot<BlogArticleId>
{
    /// <summary>
    /// 會員Id
    /// </summary>
    public MemberId MemberId { get; private set; }

    /// <summary>
    /// 文章上下架狀態
    /// </summary>
    public PublicationState PublicationState { get; private set; }

    /// <summary>
    /// 文章內容
    /// </summary>
    public ArticleContent Content { get; private set; }

    /// <summary>
    /// 文章標題
    /// </summary>
    public ArticleTitle Title { get; private set; }


    /// <summary>
    /// 文章狀態
    /// </summary>
    public ArticleState State { get; private set; }

    /// <summary>
    /// 建立文章
    /// </summary>
    /// <param name="id">文章Id</param>
    /// <param name="memberId">會員Id</param>
    public BlogArticle(BlogArticleId id, MemberId memberId)
    {
        Apply(new BlogArticleCreatedEvent
        {
            ArticleId = id,
            MemberId = memberId,
            CreateDate = DateTime.Now
        });
    }

    /// <summary>
    /// 變更內容
    /// </summary>
    /// <param name="content"></param>
    public void ChangeContent(string content)
    {
        Apply(new BlogArticleContentChangedEvent
        {
            ArticleId = Id,
            Content = content
        });
    }

    /// <summary>
    /// 變更標題
    /// </summary>
    /// <param name="title"></param>
    public void ChangeTitle(string title)
    {
        Apply(new BlogArticleTitleChangedEvent
        {
            ArticleId = Id,
            Title = title
        });
    }

    /// <summary>
    /// 上架
    /// </summary>
    public void Publish()
    {
        Apply(new BlogArticlePublishedEvent
        {
            ArticleId = Id,
        });
    }


    /// <summary>
    /// 下架
    /// </summary>
    public void Withdraw()
    {
        Apply(new BlogArticleWithdrawnEvent
        {
            ArticleId = Id
        });
    }

    /// <summary>
    /// 移除
    /// </summary>
    public void Remove()
    {
        Apply(new BlogArticleRemovedEvent
        {
            ArticleId = Id
        });
    }

    protected override void When(object @event)
    {
        switch (@event)
        {
            case BlogArticleCreatedEvent e:
                Id = new(e.ArticleId);
                MemberId = new(e.MemberId);
                PublicationState = PublicationState.Withdrawn;
                State = ArticleState.Active;
                break;

            case BlogArticleContentChangedEvent e:
                Content = ArticleContent.FromString(e.Content);
                break;

            case BlogArticleTitleChangedEvent e:
                Title = new(e.Title);
                break;

            case BlogArticlePublishedEvent:
                PublicationState = PublicationState.Published;
                break;

            case BlogArticleWithdrawnEvent:
                PublicationState = PublicationState.Withdrawn;
                break;

            case BlogArticleRemovedEvent:
                State = ArticleState.Inactive;
                break;
        }
    }

    protected override void EnsureValidState()
    {
        var valid = Id != Guid.Empty && MemberId != Guid.Empty && PublicationState switch
        {
            PublicationState.Published => Content is not null && Title is not null && State == ArticleState.Active,
            _ => true
        };

        if (!valid)
        {
            throw new BlogArticleDomainException($"Post-check fail in PublicationState {PublicationState}");
        }
    }
}