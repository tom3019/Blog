using System;
using System.Threading.Tasks;
using Blog.BlogArticles.Entities;
using Blog.BlogArticles.UseCase;
using Blog.BlogArticles.UseCase.Exceptions;
using Blog.BlogArticles.UseCase.Port.In.MemberEditTitle;
using Blog.BlogArticles.UseCase.Port.Out;
using Blog.SeedWork;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Blog.BlogArticles.UseCaseTest;

public class MemberEditTitleServiceTest
{
    private readonly IDomainEventBus _domainEventBus;
    private readonly ILoadBlogArticlePort _loadBlogArticlePort;
    private readonly ISaveBlogArticlePort _saveBlogArticlePort;

    public MemberEditTitleServiceTest()
    {
        _loadBlogArticlePort = Substitute.For<ILoadBlogArticlePort>();
        _domainEventBus = Substitute.For<IDomainEventBus>();
        _saveBlogArticlePort = Substitute.For<ISaveBlogArticlePort>();
    }

    private IMemberEditTitleService GetSystemUnderTest()
    {
        return new MemberEditTitleService(_loadBlogArticlePort, _domainEventBus, _saveBlogArticlePort);
    }

    [Fact]
    public async Task 編輯標題_部落格文章不存在_拋出BlogArticleNotFoundException()
    {
        // Arrange
        _loadBlogArticlePort.LoadAsync(Arg.Any<BlogArticleId>()).Returns(BlogArticle.Null);

        // Act
        var sut = GetSystemUnderTest();
        var actual = async () => await sut.HandleAsync(
            new MemberEditTitleImport
            {
                BlogArticleId = Guid.NewGuid(),
                Title = "Test",
            });

        // Assert
        await actual.Should().ThrowAsync<BlogArticleNotFoundException>();
    }

    [Fact]
    public async Task 編輯標題_儲存失敗_傳回False與ErrorMessage()
    {
        // Arrange
        _loadBlogArticlePort.LoadAsync(Arg.Any<BlogArticleId>()).Returns(
            new BlogArticle(new BlogArticleId(Guid.NewGuid()), new MemberId(Guid.NewGuid())));
        _saveBlogArticlePort.SaveAsync(Arg.Any<BlogArticle>()).Returns(false);

        // Act
        var sut = GetSystemUnderTest();
        var actual = await sut.HandleAsync(
            new MemberEditTitleImport
            {
                BlogArticleId = Guid.NewGuid(),
                Title = "Test",
            });

        // Assert
        actual.Success.Should().BeFalse();
        actual.ErrorMessage.Should().Be("Save blog article fail.");
    }

    [Fact]
    public async Task 編輯標題_儲存成功_傳回True()
    {
        // Arrange
        _loadBlogArticlePort.LoadAsync(Arg.Any<BlogArticleId>()).Returns(
            new BlogArticle(new BlogArticleId(Guid.NewGuid()), new MemberId(Guid.NewGuid())));
        _saveBlogArticlePort.SaveAsync(Arg.Any<BlogArticle>()).Returns(true);

        // Act
        var sut = GetSystemUnderTest();
        var actual = await sut.HandleAsync(
            new MemberEditTitleImport
            {
                BlogArticleId = Guid.NewGuid(),
                Title = "Test",
            });

        // Assert
        actual.Success.Should().BeTrue();
    }
}