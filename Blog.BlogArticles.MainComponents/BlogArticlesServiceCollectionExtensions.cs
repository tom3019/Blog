using Blog.BlogArticles.Adapter.Out;
using Blog.BlogArticles.Entities;
using Blog.BlogArticles.UseCase;
using Blog.BlogArticles.UseCase.Port.In.ArticlePublish;
using Blog.BlogArticles.UseCase.Port.In.ArticleWithdraw;
using Blog.BlogArticles.UseCase.Port.In.MemberCreateArticle;
using Blog.BlogArticles.UseCase.Port.In.MemberEditContent;
using Blog.BlogArticles.UseCase.Port.In.MemberEditTitle;
using Blog.BlogArticles.UseCase.Port.In.RemoveArticle;
using Blog.BlogArticles.UseCase.Port.Out;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.BlogArticles.MainComponents;

public static class BlogArticlesServiceCollectionExtensions
{
    public static IServiceCollection AddBlogArticlesModule(this IServiceCollection services)
    {
        return services.AddImportModule().AddOutPortModule();

    }

    private static IServiceCollection AddImportModule(this IServiceCollection services)
    {
        services.AddScoped<IMemberCreateArticleService, MemberCreateArticleService>();
        services.AddScoped<IMemberEditContentService, MemberEditContentService>();
        services.AddScoped<IMemberEditTitleService, MemberEditTitleService>();
        services.AddScoped<IArticlePublishService, ArticlePublishService>();
        services.AddScoped<IArticleWithdrawService, ArticleWithdrawService>();
        services.AddScoped<IArticleRemoveService, ArticleRemoveService>();
        return services;
    }

    private static IServiceCollection AddOutPortModule(this IServiceCollection services)
    {
        services.AddScoped<IGetNewBlogArticleIdPort, BlogArticlesRepository>();
        services.AddScoped<ILoadBlogArticlePort, BlogArticlesRepository>();
        services.AddScoped<ISaveBlogArticlePort, BlogArticlesRepository>();
        return services;
    }
}