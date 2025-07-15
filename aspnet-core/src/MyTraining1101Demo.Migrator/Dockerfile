#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/MyTraining1101Demo.Migrator/MyTraining1101Demo.Migrator.csproj", "src/MyTraining1101Demo.Migrator/"]
COPY ["src/MyTraining1101Demo.EntityFrameworkCore/MyTraining1101Demo.EntityFrameworkCore.csproj", "src/MyTraining1101Demo.EntityFrameworkCore/"]
COPY ["src/MyTraining1101Demo.Core/MyTraining1101Demo.Core.csproj", "src/MyTraining1101Demo.Core/"]
COPY ["src/MyTraining1101Demo.Core.Shared/MyTraining1101Demo.Core.Shared.csproj", "src/MyTraining1101Demo.Core.Shared/"]
RUN dotnet restore "src/MyTraining1101Demo.Migrator/MyTraining1101Demo.Migrator.csproj"
COPY . .
WORKDIR "/src/src/MyTraining1101Demo.Migrator"
RUN dotnet build "MyTraining1101Demo.Migrator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MyTraining1101Demo.Migrator.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyTraining1101Demo.Migrator.dll", "-s"]
