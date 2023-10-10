using Blog.SeedWork;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Application.Models.BlogArticles.Request;

public class EditTitleRequest:IRequest
{
    public Guid ArticleId { get; set; }

    public string Title { get; set; }
}