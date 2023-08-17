FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal AS base
ARG PROJECT_PORT
WORKDIR /app
EXPOSE $PROJECT_PORT

FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /src
COPY ["Squirrel.Core/Squirrel.Core.WebAPI/Squirrel.Core.WebAPI.csproj", "Squirrel.Core/Squirrel.Core.WebAPI/"]
COPY ["Squirrel.Core/Squirrel.Core.BLL/Squirrel.Core.BLL.csproj", "Squirrel.Core/Squirrel.Core.BLL/"]
COPY ["Squirrel.Core/Squirrel.Core.DAL/Squirrel.Core.DAL.csproj", "Squirrel.Core/Squirrel.Core.DAL/"]
COPY ["Squirrel.Core/Squirrel.Core.Common/Squirrel.Core.Common.csproj", "Squirrel.Core/Squirrel.Core.Common/"]

RUN dotnet restore "Squirrel.Core/Squirrel.Core.WebAPI/Squirrel.Core.WebAPI.csproj"
COPY . .
WORKDIR "/src/Squirrel.Core/Squirrel.Core.WebAPI"
RUN dotnet build "Squirrel.Core.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Squirrel.Core.WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS http://*:5050
ENTRYPOINT ["dotnet", "Squirrel.Core.WebAPI.dll"]