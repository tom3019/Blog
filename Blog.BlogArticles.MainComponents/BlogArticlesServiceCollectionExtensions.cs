using Blog.BlogArticles.Entities;
using Blog.BlogArticles.UseCase;
using Blog.BlogArticles.UseCase.Port.In.MemberCreateArticle;
using Blog.BlogArticles.UseCase.Port.In.MemberEditContent;
using Blog.BlogArticles.UseCase.Port.In.MemberEditTitle;
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
        return services;
    }

    private static IServiceCollection AddOutPortModule(this IServiceCollection services)
    {
        return services;
    }
}