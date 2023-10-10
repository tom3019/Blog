using Blog.Application.Controllers;
using Blog.Application.Models.BlogArticles.Request;
using Blog.BlogArticles.UseCase.Port.In.ArticlePublish;
using Blog.BlogArticles.UseCase.Port.In.ArticleWithdraw;
using Blog.BlogArticles.UseCase.Port.In.MemberCreateArticle;
using Blog.BlogArticles.UseCase.Port.In.MemberEditContent;
using Blog.BlogArticles.UseCase.Port.In.MemberEditTitle;
using Blog.BlogArticles.UseCase.Port.In.RemoveArticle;
using Blog.SeedWork;

namespace Blog.Application.Infra;

public class BlogMediator : IMediator
{
    private readonly IMemberCreateArticleService _memberCreateArticleService;
    private readonly IMemberEditTitleService _memberEditTitleService;
    private readonly IMemberEditContentService _memberEditContentService;
    private readonly IArticlePublishService _articlePublishService;
    private readonly IArticleWithdrawService _articleWithdrawService;
    private readonly IArticleRemoveService _articleRemoveService;

    public BlogMediator(IMemberCreateArticleService memberCreateArticleService,
        IMemberEditTitleService memberEditTitleService,
        IMemberEditContentService memberEditContentService,
        IArticlePublishService articlePublishService,
        IArticleWithdrawService articleWithdrawService,
        IArticleRemoveService articleRemoveService)
    {
        _memberCreateArticleService = memberCreateArticleService;
        _memberEditTitleService = memberEditTitleService;
        _memberEditContentService = memberEditContentService;
        _articlePublishService = articlePublishService;
        _articleWithdrawService = articleWithdrawService;
        _articleRemoveService = articleRemoveService;
    }

    public async Task<IResponse> SendAsync(IRequest request)
    {
        return request switch
        {
            CreateArticleRequest r => await _memberCreateArticleService.HandleAsync(
                new MemberCreateArticleImport
                {
                    MemberId = r.MemberId
                }),

            EditTitleRequest r => await _memberEditTitleService.HandleAsync(
                new MemberEditTitleImport
                {
                    BlogArticleId = r.ArticleId,
                    Title = r.Title
                }),

            EditContentRequest r => await _memberEditContentService.HandleAsync(
                new MemberEditContentImport
                {
                    BlogArticleId = r.ArticleId,
                    Content = r.Content
                }),

            PublishArticleRequest r => await _articlePublishService.HandleAsync(
                new ArticlePublishImport
                {
                    BlogArticleId = r.ArticleId
                }),

            WithdrawArticleRequest r => await _articleWithdrawService.HandleAsync(
                new ArticleWithdrawImport
                {
                    BlogArticleId = r.ArticleId
                }),

            DeleteArticleRequest r => await _articleRemoveService.HandleAsync(
                new ArticleRemoveImport
                {
                    BlogArticleId = r.ArticleId
                }),

            _ => throw new ArgumentOutOfRangeException(nameof(request), request, null)
        };
    }
}