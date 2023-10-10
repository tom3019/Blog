using Blog.Application.Models.BlogArticles.Request;
using Blog.BlogArticles.UseCase.Port.In.MemberCreateArticle;
using Blog.SeedWork;

namespace Blog.Application.Infra;

public class BlogMediator : IMediator
{
    private readonly IMemberCreateArticleService _memberCreateArticleService;

    public BlogMediator(IMemberCreateArticleService memberCreateArticleService)
    {
        _memberCreateArticleService = memberCreateArticleService;
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
            _ => throw new ArgumentOutOfRangeException(nameof(request), request, null)
        };
    }
}