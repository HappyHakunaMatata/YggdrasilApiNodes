#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8081

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG TARGETARCH
ARG BUILDPLATFORM
WORKDIR /src
COPY ["YggdrasilApiNodes/YggdrasilApiNodes.csproj", "YggdrasilApiNodes/"]
RUN dotnet restore "YggdrasilApiNodes/YggdrasilApiNodes.csproj"
COPY . .
WORKDIR "/src/YggdrasilApiNodes"
RUN dotnet build "YggdrasilApiNodes.csproj" -c Release -o /app/build -a $TARGETARCH

FROM build AS publish
RUN dotnet publish "YggdrasilApiNodes.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM --platform=$BUILDPLATFORM base AS final
ARG TARGETARCH
ARG BUILDPLATFORM
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "YggdrasilApiNodes.dll"]

