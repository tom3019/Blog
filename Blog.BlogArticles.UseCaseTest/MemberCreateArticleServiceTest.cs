using System;
using System.Threading.Tasks;
using Blog.BlogArticles.Entities;
using Blog.BlogArticles.UseCase;
using Blog.BlogArticles.UseCase.Port.In.MemberCreateArticle;
using Blog.BlogArticles.UseCase.Port.Out;
using Blog.SeedWork;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Blog.BlogArticles.UseCaseTest;

public class MemberCreateArticleServiceTest
{
    private readonly IDomainEventBus _domainEventBus;
    private readonly IGetNewBlogArticleIdPort _getNewBlogArticleIdPort;
    private readonly ISaveBlogArticlePort _saveBlogArticlePort;

    public MemberCreateArticleServiceTest()
    {
        _domainEventBus = Substitute.For<IDomainEventBus>();
        _saveBlogArticlePort = Substitute.For<ISaveBlogArticlePort>();
        _getNewBlogArticleIdPort = Substitute.For<IGetNewBlogArticleIdPort>();
    }

    private IMemberCreateArticleService GetSystemUnderTest()
    {
        return new MemberCreateArticleService(_domainEventBus, _saveBlogArticlePort, _getNewBlogArticleIdPort);
    }

    [Fact]
    public async Task 建立部落格文章_資料庫儲存失敗_傳回False與ErrorMessage()
    {
        _getNewBlogArticleIdPort.NewBlogArticleIdAsync().Returns(new Guid("20A5836B-5396-4C59-96E5-AEF08BAC5B4B"));
        _saveBlogArticlePort.SaveAsync(Arg.Any<BlogArticle>()).Returns(false);

        var sut = GetSystemUnderTest();
        var actual = await sut.HandleAsync(new MemberCreateArticleImport
        {
            MemberId = new Guid("83460E3B-79F7-41EE-BDF3-CE81A1478577"),
        });

        actual.Success.Should().BeFalse();
        actual.ErrorMessage.Should().Be("Save blog article fail.");
    }

    [Fact]
    public async Task 建立部落格文章_資料庫儲存成功_傳回True()
    {
        _getNewBlogArticleIdPort.NewBlogArticleIdAsync().Returns(new Guid("20A5836B-5396-4C59-96E5-AEF08BAC5B4B"));
        _saveBlogArticlePort.SaveAsync(Arg.Any<BlogArticle>()).Returns(true);

        var sut = GetSystemUnderTest();
        var actual = await sut.HandleAsync(new MemberCreateArticleImport
        {
            MemberId = new Guid("83460E3B-79F7-41EE-BDF3-CE81A1478577"),
        });

        actual.Success.Should().BeTrue();
    }
}