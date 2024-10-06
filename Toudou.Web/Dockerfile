FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Toudou.Web/Toudou.Web.csproj", "Toudou.Web/"]
RUN dotnet restore "Toudou.Web/Toudou.Web.csproj"
COPY . .
WORKDIR "/src/Toudou.Web"
RUN dotnet build "Toudou.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Toudou.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Toudou.Web.dll"]
