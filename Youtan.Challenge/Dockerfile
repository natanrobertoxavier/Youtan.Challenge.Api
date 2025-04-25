FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /main
COPY ["Youtan.Challenge.Api/Youtan.Challenge.Api.csproj", "Youtan.Challenge.Api/"]
RUN dotnet restore "Youtan.Challenge.Api/Youtan.Challenge.Api.csproj"
COPY . .

RUN dotnet build "Youtan.Challenge.Api/Youtan.Challenge.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Youtan.Challenge.Api/Youtan.Challenge.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Youtan.Challenge.Api.dll"]