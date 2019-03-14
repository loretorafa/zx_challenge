FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["ZX_Challenge.Api/ZX_Challenge.Api.csproj", "ZX_Challenge.Api/"]
COPY ["ZX_Challenge.Application/ZX_Challenge.Application.csproj", "ZX_Challenge.Application/"]
COPY ["ZX_Challenge.Domain/ZX_Challenge.Domain.csproj", "ZX_Challenge.Domain/"]
COPY ["ZX_Challenge.Infrastructure/ZX_Challenge.Infrastructure.csproj", "ZX_Challenge.Infrastructure/"]
RUN dotnet restore "ZX_Challenge.Api/ZX_Challenge.Api.csproj"
COPY . .
WORKDIR "/src/ZX_Challenge.Api"
RUN dotnet build "ZX_Challenge.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ZX_Challenge.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ZX_Challenge.Api.dll"]