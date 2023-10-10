FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Blog.Application/Blog.Application.csproj", "Blog.Application/"]
RUN dotnet restore "Blog.Application/Blog.Application.csproj"
COPY . .
WORKDIR "/src/Blog.Application"
RUN dotnet build "Blog.Application.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Blog.Application.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Blog.Application.dll"]
