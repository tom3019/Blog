using System;
using System.Threading.Tasks;
using Blog.BlogArticles.Entities;
using Blog.BlogArticles.UseCase;
using Blog.BlogArticles.UseCase.Exceptions;
using Blog.BlogArticles.UseCase.Port.In.RemoveArticle;
using Blog.BlogArticles.UseCase.Port.Out;
using Blog.SeedWork;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Xunit;

namespace Blog.BlogArticles.UseCaseTest;

public class ArticleRemoveServiceTest
{
    private readonly IDomainEventBus _domainEventBus;
    private readonly ILoadBlogArticlePort _loadBlogArticlePort;
    private readonly ISaveBlogArticlePort _saveBlogArticlePort;

    public ArticleRemoveServiceTest()
    {
        _loadBlogArticlePort = Substitute.For<ILoadBlogArticlePort>();
        _domainEventBus = Substitute.For<IDomainEventBus>();
        _saveBlogArticlePort = Substitute.For<ISaveBlogArticlePort>();
    }

    private IArticleRemoveService GetSystemUnderTest()
    {
        return new ArticleRemoveService(_loadBlogArticlePort, _domainEventBus, _saveBlogArticlePort);
    }

    [Fact]
    public async Task 文章刪除_部落格文章不存在_拋出BlogArticleNotFoundException()
    {
        // Arrange
        _loadBlogArticlePort.LoadAsync(Arg.Any<BlogArticleId>()).Returns(BlogArticle.Null);

        // Act
        var sut = GetSystemUnderTest();
        var actual = async () => await sut.HandleAsync(new ArticleRemoveImport
        {
            BlogArticleId = Guid.NewGuid(),
        });

        // Assert
        await actual.Should().ThrowAsync<BlogArticleNotFoundException>();
    }

    [Fact]
    public async Task 文章刪除_儲存失敗_傳回False與ErrorMessage()
    {
        // Arrange
        _loadBlogArticlePort.LoadAsync(Arg.Any<BlogArticleId>()).Returns(
            new BlogArticle(new BlogArticleId(new Guid("9684C1A0-17F9-4CA1-8475-B4EFD31B2608")),
                new MemberId(new Guid("F758AE76-1F5D-4698-BC31-EE756633A876"))));

        _saveBlogArticlePort.SaveAsync(Arg.Any<BlogArticle>()).Returns(false);

        // Act
        var sut = GetSystemUnderTest();
        var actual = await sut.HandleAsync(new ArticleRemoveImport
        {
            BlogArticleId = new Guid("9684C1A0-17F9-4CA1-8475-B4EFD31B2608"),
        });

        // Assert
        actual.Success.Should().BeFalse();
        actual.ErrorMessage.Should().Be("Save blog article fail.");
    }

    [Fact]
    public async Task 文章刪除_儲存成功_傳回True()
    {
        // Arrange
        _loadBlogArticlePort.LoadAsync(Arg.Any<BlogArticleId>()).Returns(
            new BlogArticle(new BlogArticleId(new Guid("9684C1A0-17F9-4CA1-8475-B4EFD31B2608")),
                new MemberId(new Guid("F758AE76-1F5D-4698-BC31-EE756633A876"))));

        _saveBlogArticlePort.SaveAsync(Arg.Any<BlogArticle>()).Returns(true);

        // Act
        var sut = GetSystemUnderTest();
        var actual = await sut.HandleAsync(new ArticleRemoveImport
        {
            BlogArticleId = new Guid("9684C1A0-17F9-4CA1-8475-B4EFD31B2608"),
        });

        // Assert
        actual.Success.Should().BeTrue();
    }
}