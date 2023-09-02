using Blog.BlogArticles.Entities;
using Blog.BlogArticles.UseCase.Port.Out;

namespace Blog.BlogArticles.Adapter.Out;

public class BlogArticlesRepository : IGetNewBlogArticleIdPort , ILoadBlogArticlePort , ISaveBlogArticlePort
{
    public Task<Guid> GetNewBlogArticleIdAsync()
    {
        throw new NotImplementedException();
    }

    public Task<BlogArticle> LoadAsync(BlogArticleId id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> SaveAsync(BlogArticle blogArticle)
    {
        throw new NotImplementedException();
    }
}