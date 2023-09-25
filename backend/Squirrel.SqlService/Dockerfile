FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal AS base
ARG PROJECT_PORT
WORKDIR /app
EXPOSE $PROJECT_PORT

FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /src
COPY ["Squirrel.SqlService/Squirrel.SqlService.WebApi/Squirrel.SqlService.WebApi.csproj", "Squirrel.SqlService/Squirrel.SqlService.WebApi/"]
COPY ["Squirrel.SqlService/Squirrel.SqlService.BLL/Squirrel.SqlService.BLL.csproj", "Squirrel.SqlService/Squirrel.SqlService.BLL/"]
COPY ["Squirrel.Core/Squirrel.Core.DAL/Squirrel.Core.DAL.csproj", "Squirrel.Core/Squirrel.Core.DAL/"]
COPY ["Squirrel.AzureBlobStorage/Squirrel.AzureBlobStorage.csproj", "Squirrel.AzureBlobStorage/"]
COPY ["Squirrel.Shared/Squirrel.Shared.csproj", "Squirrel.Shared/"]

RUN dotnet restore "Squirrel.SqlService/Squirrel.SqlService.WebApi/Squirrel.SqlService.WebApi.csproj"
COPY . .
WORKDIR "/src/Squirrel.SqlService/Squirrel.SqlService.WebApi"
RUN dotnet build "Squirrel.SqlService.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Squirrel.SqlService.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS http://*:5076
ENTRYPOINT ["dotnet", "Squirrel.SqlService.WebApi.dll"]