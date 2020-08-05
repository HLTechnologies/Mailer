#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/Mailer.Send.Service/Mailer.Send.Service.csproj", "src/Mailer.Send.Service/"]
RUN dotnet restore "src/Mailer.Send.Service/Mailer.Send.Service.csproj"
COPY . .
WORKDIR "/src/src/Mailer.Send.Service"
RUN dotnet build "Mailer.Send.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Mailer.Send.Service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Mailer.Send.Service.dll"]