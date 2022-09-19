#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Web/InfinityCinema.Web/InfinityCinema.Web.csproj", "Web/InfinityCinema.Web/"]
COPY ["InfinityCinema.Common/InfinityCinema.Common.csproj", "InfinityCinema.Common/"]
COPY ["Data/InfinityCinema.Data.Models/InfinityCinema.Data.Models.csproj", "Data/InfinityCinema.Data.Models/"]
COPY ["Data/InfinityCinema.Data.Common/InfinityCinema.Data.Common.csproj", "Data/InfinityCinema.Data.Common/"]
COPY ["Data/InfinityCinema.Data/InfinityCinema.Data.csproj", "Data/InfinityCinema.Data/"]
COPY ["Services/InfinityCinema.Services.Data/InfinityCinema.Services.Data.csproj", "Services/InfinityCinema.Services.Data/"]
COPY ["Services/InfinityCinema.Services.Mapping/InfinityCinema.Services.Mapping.csproj", "Services/InfinityCinema.Services.Mapping/"]
COPY ["Services/InfinityCinema.Services.Messaging/InfinityCinema.Services.Messaging.csproj", "Services/InfinityCinema.Services.Messaging/"]
COPY ["Services/InfinityCinema.Services/InfinityCinema.Services.csproj", "Services/InfinityCinema.Services/"]
COPY ["Web/InfinityCinema.Web.Infrastructure/InfinityCinema.Web.Infrastructure.csproj", "Web/InfinityCinema.Web.Infrastructure/"]
COPY ["Web/InfinityCinema.Web.ViewModels/InfinityCinema.Web.ViewModels.csproj", "Web/InfinityCinema.Web.ViewModels/"]
RUN dotnet restore "Web/InfinityCinema.Web/InfinityCinema.Web.csproj"
COPY . .
WORKDIR "/src/Web/InfinityCinema.Web"
RUN dotnet build "InfinityCinema.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "InfinityCinema.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InfinityCinema.Web.dll"]