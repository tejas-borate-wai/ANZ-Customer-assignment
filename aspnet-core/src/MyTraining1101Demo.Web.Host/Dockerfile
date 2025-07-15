#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/MyTraining1101Demo.Web.Host/MyTraining1101Demo.Web.Host.csproj", "src/MyTraining1101Demo.Web.Host/"]
COPY ["src/MyTraining1101Demo.Web.Core/MyTraining1101Demo.Web.Core.csproj", "src/MyTraining1101Demo.Web.Core/"]
COPY ["src/MyTraining1101Demo.Application/MyTraining1101Demo.Application.csproj", "src/MyTraining1101Demo.Application/"]
COPY ["src/MyTraining1101Demo.Application.Shared/MyTraining1101Demo.Application.Shared.csproj", "src/MyTraining1101Demo.Application.Shared/"]
COPY ["src/MyTraining1101Demo.Core.Shared/MyTraining1101Demo.Core.Shared.csproj", "src/MyTraining1101Demo.Core.Shared/"]
COPY ["src/MyTraining1101Demo.Core/MyTraining1101Demo.Core.csproj", "src/MyTraining1101Demo.Core/"]
COPY ["src/MyTraining1101Demo.EntityFrameworkCore/MyTraining1101Demo.EntityFrameworkCore.csproj", "src/MyTraining1101Demo.EntityFrameworkCore/"]
COPY ["src/MyTraining1101Demo.GraphQL/MyTraining1101Demo.GraphQL.csproj", "src/MyTraining1101Demo.GraphQL/"]
RUN dotnet restore "src/MyTraining1101Demo.Web.Host/MyTraining1101Demo.Web.Host.csproj"
COPY . .
WORKDIR "/src/src/MyTraining1101Demo.Web.Host"
RUN dotnet build "MyTraining1101Demo.Web.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MyTraining1101Demo.Web.Host.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyTraining1101Demo.Web.Host.dll"]
