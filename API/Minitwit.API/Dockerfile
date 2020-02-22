#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
#FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build


FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
RUN export ASPNETCORE_ENVIRONMENT=Production

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
ENV ASPNETCORE_ENVIRONMENT=Production
WORKDIR /src
COPY ["Minitwit.API/Minitwit.API.csproj", "Minitwit.API/"]
RUN dotnet restore "Minitwit.API/Minitwit.API.csproj"
COPY . .
WORKDIR "/src/Minitwit.API"
RUN dotnet build "Minitwit.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Minitwit.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
RUN export ASPNETCORE_ENVIRONMENT=Production
ENTRYPOINT ["dotnet", "Minitwit.API.dll"]