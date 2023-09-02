using System;
using System.Threading.Tasks;
using Blog.BlogArticles.Entities;
using Blog.BlogArticles.UseCase;
using Blog.BlogArticles.UseCase.Exceptions;
using Blog.BlogArticles.UseCase.Port.In.ArticleWithdraw;
using Blog.BlogArticles.UseCase.Port.Out;
using Blog.SeedWork;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Xunit;

namespace Blog.BlogArticles.UseCaseTest;

public class ArticleWithdrawServiceTest
{
    private readonly IDomainEventBus _domainEventBus;
    private readonly ILoadBlogArticlePort _loadBlogArticlePort;
    private readonly ISaveBlogArticlePort _saveBlogArticlePort;

    public ArticleWithdrawServiceTest()
    {
        _domainEventBus = Substitute.For<IDomainEventBus>();
        _loadBlogArticlePort = Substitute.For<ILoadBlogArticlePort>();
        _saveBlogArticlePort = Substitute.For<ISaveBlogArticlePort>();
    }

    private IArticleWithdrawService GetSystemUnderTest()
    {
        return new ArticleWithdrawService(_domainEventBus, _loadBlogArticlePort, _saveBlogArticlePort);
    }

    [Fact]
    public void 文章下架_部落格文章不存在_拋出BlogArticleNotFoundException()
    {
        // Arrange
        var import = new ArticleWithdrawImport
        {
            BlogArticleId = Guid.NewGuid(),
        };
        _loadBlogArticlePort.LoadAsync(Arg.Any<BlogArticleId>()).ReturnsNull();

        // Act
        var sut = GetSystemUnderTest();
        var actual = async () => await sut.HandleAsync(import);

        // Assert
        actual.Should().ThrowAsync<BlogArticleNotFoundException>();
    }

    [Fact]
    public async Task 文章下架_儲存失敗_傳回False與ErrorMessage()
    {
        // Arrange
        _loadBlogArticlePort.LoadAsync(Arg.Any<BlogArticleId>()).Returns(
            new BlogArticle(new BlogArticleId(new Guid("9684C1A0-17F9-4CA1-8475-B4EFD31B2608")),
                new MemberId(new Guid("F758AE76-1F5D-4698-BC31-EE756633A876"))));

        _saveBlogArticlePort.SaveAsync(Arg.Any<BlogArticle>()).Returns(false);

        // Act
        var sut = GetSystemUnderTest();
        var actual = await sut.HandleAsync(new ArticleWithdrawImport
        {
            BlogArticleId = Guid.NewGuid(),
        });

        // Assert
        actual.Success.Should().BeFalse();
        actual.ErrorMessage.Should().Be("Save blog article fail.");
    }

    [Fact]
    public async Task 文章下架_儲存成功_傳回True()
    {
        // Arrange
        _loadBlogArticlePort.LoadAsync(Arg.Any<BlogArticleId>()).Returns(
            new BlogArticle(new BlogArticleId(new Guid("9684C1A0-17F9-4CA1-8475-B4EFD31B2608")),
                new MemberId(new Guid("F758AE76-1F5D-4698-BC31-EE756633A876"))));

        _saveBlogArticlePort.SaveAsync(Arg.Any<BlogArticle>()).Returns(true);

        // Act
        var sut = GetSystemUnderTest();
        var actual = await sut.HandleAsync(new ArticleWithdrawImport
        {
            BlogArticleId = Guid.NewGuid(),
        });

        // Assert
        actual.Success.Should().BeTrue();
    }
}