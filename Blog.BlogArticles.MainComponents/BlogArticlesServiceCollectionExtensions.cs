using Blog.BlogArticles.Entities;
using Blog.BlogArticles.UseCase;
using Blog.BlogArticles.UseCase.Port.In.MemberCreateArticle;
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
        return services;
    }

    private static IServiceCollection AddOutPortModule(this IServiceCollection services)
    {
        return services;
    }
}