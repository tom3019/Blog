using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Blog.SeedWork;

public  abstract record ValueObject<T> where T : ValueObject<T>
{
}