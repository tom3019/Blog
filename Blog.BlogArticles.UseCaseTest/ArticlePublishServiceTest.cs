using System;
using System.Threading.Tasks;
using Blog.BlogArticles.Entities;
using Blog.BlogArticles.Entities.Exceptions;
using Blog.BlogArticles.UseCase;
using Blog.BlogArticles.UseCase.Exceptions;
using Blog.BlogArticles.UseCase.Port.In.ArticlePublish;
using Blog.BlogArticles.UseCase.Port.Out;
using Blog.SeedWork;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Xunit;

namespace Blog.BlogArticles.UseCaseTest;

public class ArticlePublishServiceTest
{
    private readonly IDomainEventBus _domainEventBus;
    private readonly ILoadBlogArticlePort _loadBlogArticlePort;
    private readonly ISaveBlogArticlePort _saveBlogArticlePort;

    public ArticlePublishServiceTest()
    {
        _loadBlogArticlePort = Substitute.For<ILoadBlogArticlePort>();
        _domainEventBus = Substitute.For<IDomainEventBus>();
        _saveBlogArticlePort = Substitute.For<ISaveBlogArticlePort>();
    }

    private IArticlePublishService GetSystemUnderTest()
    {
        return new ArticlePublishService(_loadBlogArticlePort, _domainEventBus, _saveBlogArticlePort);
    }

    [Fact]
    public async Task 文章上架_部落格文章不存在_拋出BlogArticleNotFoundException()
    {
        // Arrange
        _loadBlogArticlePort.LoadAsync(Arg.Any<BlogArticleId>()).Returns(BlogArticle.Null);

        // Act
        var sut = GetSystemUnderTest();
        var actual = async () => await sut.HandleAsync(new ArticlePublishImport
        {
            BlogArticleId = Guid.NewGuid(),
        });

        // Assert
        await actual.Should().ThrowAsync<BlogArticleNotFoundException>();
    }

    [Fact]
    public async Task 文章上架_儲存失敗_傳回False與ErrorMessage()
    {
        // Arrange
        var blogArticle = new BlogArticle(new BlogArticleId(new Guid("9684C1A0-17F9-4CA1-8475-B4EFD31B2608")),
            new MemberId(new Guid("F758AE76-1F5D-4698-BC31-EE756633A876")));
        blogArticle.ChangeContent("content");
        blogArticle.ChangeTitle("title");

        _loadBlogArticlePort.LoadAsync(Arg.Any<BlogArticleId>()).Returns(blogArticle);

        _saveBlogArticlePort.SaveAsync(Arg.Any<BlogArticle>()).Returns(false);

        // Act
        var sut = GetSystemUnderTest();
        var actual = await sut.HandleAsync(new ArticlePublishImport
        {
            BlogArticleId = new Guid("9684C1A0-17F9-4CA1-8475-B4EFD31B2608"),
        });

        // Assert
        actual.Success.Should().BeFalse();
        actual.ErrorMessage.Should().Be("Save blog article fail.");
    }

    [Fact]
    public async Task 文章上架_儲存成功_傳回True()
    {
        // Arrange
        var blogArticle = new BlogArticle(new BlogArticleId(new Guid("9684C1A0-17F9-4CA1-8475-B4EFD31B2608")),
            new MemberId(new Guid("F758AE76-1F5D-4698-BC31-EE756633A876")));
        blogArticle.ChangeContent("content");
        blogArticle.ChangeTitle("title");

        _loadBlogArticlePort.LoadAsync(Arg.Any<BlogArticleId>()).Returns(blogArticle);

        _saveBlogArticlePort.SaveAsync(Arg.Any<BlogArticle>()).Returns(true);

        // Act
        var sut = GetSystemUnderTest();
        var actual = await sut.HandleAsync(new ArticlePublishImport
        {
            BlogArticleId = new Guid("9684C1A0-17F9-4CA1-8475-B4EFD31B2608"),
        });

        // Assert
        actual.Success.Should().BeTrue();
    }

    [Fact]
    public void 文章上架_文章無內容_拋出BlogArticleDomainException()
    {
        // Arrange
        _loadBlogArticlePort.LoadAsync(Arg.Any<BlogArticleId>()).Returns(new BlogArticle(
            new BlogArticleId(new Guid("9684C1A0-17F9-4CA1-8475-B4EFD31B2608")),
            new MemberId(new Guid("F758AE76-1F5D-4698-BC31-EE756633A876"))));

        // Act
        var sut = GetSystemUnderTest();
        var actual = async () => await sut.HandleAsync(new ArticlePublishImport
        {
            BlogArticleId = Guid.NewGuid(),
        });

        // Assert
        actual.Should().ThrowAsync<BlogArticleDomainException>();
    }

    [Fact]
    public void 文章上架_文章無標題_拋出BlogArticleDomainException()
    {
        // Arrange
        _loadBlogArticlePort.LoadAsync(Arg.Any<BlogArticleId>()).Returns(new BlogArticle(
            new BlogArticleId(new Guid("9684C1A0-17F9-4CA1-8475-B4EFD31B2608")),
            new MemberId(new Guid("F758AE76-1F5D-4698-BC31-EE756633A876"))));

        // Act
        var sut = GetSystemUnderTest();
        var actual = async () => await sut.HandleAsync(new ArticlePublishImport
        {
            BlogArticleId = Guid.NewGuid(),
        });

        // Assert
        actual.Should().ThrowAsync<BlogArticleDomainException>();
    }

    [Fact]
    public void 文章上架_文章已刪除_拋出BlogArticleDomainException()
    {
        // Arrange
        var blogArticle = new BlogArticle(new BlogArticleId(new Guid("9684C1A0-17F9-4CA1-8475-B4EFD31B2608")),
            new MemberId(new Guid("F758AE76-1F5D-4698-BC31-EE756633A876")));
        blogArticle.ChangeContent("content");
        blogArticle.ChangeTitle("title");
        blogArticle.Remove();

        _loadBlogArticlePort.LoadAsync(Arg.Any<BlogArticleId>()).Returns(blogArticle);

        // Act
        var sut = GetSystemUnderTest();
        var actual = async () => await sut.HandleAsync(new ArticlePublishImport
        {
            BlogArticleId = Guid.NewGuid(),
        });

        // Assert
        actual.Should().ThrowAsync<BlogArticleDomainException>();
    }
}