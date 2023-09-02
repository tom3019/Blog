using System;
using System.Threading.Tasks;
using Blog.BlogArticles.Entities;
using Blog.BlogArticles.UseCase;
using Blog.BlogArticles.UseCase.Exceptions;
using Blog.BlogArticles.UseCase.Port.In.MemberEditContent;
using Blog.BlogArticles.UseCase.Port.Out;
using Blog.SeedWork;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Xunit;

namespace Blog.BlogArticles.UseCaseTest;

public class MemberEditContentServiceTest
{
    private readonly IDomainEventBus _domainEventBus;
    private readonly ILoadBlogArticlePort _loadBlogArticlePort;
    private readonly ISaveBlogArticlePort _saveBlogArticlePort;

    public MemberEditContentServiceTest()
    {
        _domainEventBus = Substitute.For<IDomainEventBus>();
        _loadBlogArticlePort = Substitute.For<ILoadBlogArticlePort>();
        _saveBlogArticlePort = Substitute.For<ISaveBlogArticlePort>();
    }

    private IMemberEditContentService GetSystemUnderTest()
    {
        return new MemberEditContentService(_domainEventBus, _loadBlogArticlePort, _saveBlogArticlePort);
    }

    [Fact]
    public void 編輯文章內容_文章Id不存在_拋出BlogArticleNotFoundException()
    {
        var sut = GetSystemUnderTest();
        _loadBlogArticlePort.LoadAsync(Arg.Any<BlogArticleId>()).ReturnsNull();

        var actual = async () => await sut.HandleAsync(new MemberEditContentImport
        {
            BlogArticleId = new Guid("9684C1A0-17F9-4CA1-8475-B4EFD31B2608"),
            Content = "Content",
        });

        actual.Should().ThrowAsync<BlogArticleNotFoundException>();
    }

    [Fact]
    public async Task 編輯文章內容_儲存失敗_傳回False與ErrorMessage()
    {
        var sut = GetSystemUnderTest();
        _loadBlogArticlePort.LoadAsync(Arg.Any<BlogArticleId>()).Returns(
            new BlogArticle(new BlogArticleId(new Guid("9684C1A0-17F9-4CA1-8475-B4EFD31B2608")),
                new MemberId(new Guid("F758AE76-1F5D-4698-BC31-EE756633A876"))));

        _saveBlogArticlePort.SaveAsync(Arg.Any<BlogArticle>()).Returns(false);

        var actual = await sut.HandleAsync(new MemberEditContentImport
        {
            BlogArticleId = new Guid("9684C1A0-17F9-4CA1-8475-B4EFD31B2608"),
            Content = "Content",
        });

        actual.Success.Should().BeFalse();
        actual.ErrorMessage.Should().Be("Save blog article fail.");
    }

    [Fact]
    public async Task 編輯文章內容_儲存成功_傳回True()
    {
        var sut = GetSystemUnderTest();
        _loadBlogArticlePort.LoadAsync(Arg.Any<BlogArticleId>()).Returns(
            new BlogArticle(new BlogArticleId(new Guid("9684C1A0-17F9-4CA1-8475-B4EFD31B2608")),
                new MemberId(new Guid("F758AE76-1F5D-4698-BC31-EE756633A876"))));

        _saveBlogArticlePort.SaveAsync(Arg.Any<BlogArticle>()).Returns(true);

        var actual = await sut.HandleAsync(new MemberEditContentImport
        {
            BlogArticleId = new Guid("9684C1A0-17F9-4CA1-8475-B4EFD31B2608"),
            Content = "Content",
        });

        actual.Success.Should().BeTrue();
    }
}