FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal AS base
ARG PROJECT_PORT
WORKDIR /app
EXPOSE $PROJECT_PORT

FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /src
COPY ["Squirrel.Notifier/Squirrel.Notifier.WebAPI/Squirrel.Notifier.WebAPI.csproj", "Squirrel.Notifier/Squirrel.Notifier.WebAPI/"]
COPY ["Squirrel.Core/Squirrel.Core.Common/Squirrel.Core.Common.csproj", "Squirrel.Core/Squirrel.Core.Common/"]

RUN dotnet restore "Squirrel.Notifier/Squirrel.Notifier.WebAPI/Squirrel.Notifier.WebAPI.csproj"
COPY . .
WORKDIR "/src/Squirrel.Notifier/Squirrel.Notifier.WebAPI"
RUN dotnet build "Squirrel.Notifier.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Squirrel.Notifier.WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS http://*:5070
ENTRYPOINT ["dotnet", "Squirrel.Notifier.WebAPI.dll"]