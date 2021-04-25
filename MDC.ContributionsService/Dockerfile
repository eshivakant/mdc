#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["MDC.ContributionsService/MDC.ContributionsService.csproj", "MDC.ContributionsService/"]
RUN dotnet restore "MDC.ContributionsService/MDC.ContributionsService.csproj"
COPY . .
WORKDIR "/src/MDC.ContributionsService"
RUN dotnet build "MDC.ContributionsService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MDC.ContributionsService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MDC.ContributionsService.dll"]