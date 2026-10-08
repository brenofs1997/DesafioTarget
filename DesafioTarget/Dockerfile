FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["DesafioTarget/DesafioTarget.csproj", "DesafioTarget/"]
RUN dotnet restore "DesafioTarget/DesafioTarget.csproj"

COPY . .
WORKDIR /src/DesafioTarget
RUN dotnet publish "DesafioTarget.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80

COPY --from=build /app/publish .

RUN useradd -u 1000 appuser || true
RUN chown -R appuser:appuser /app
USER appuser

ENTRYPOINT ["dotnet", "DesafioTarget.dll"]
