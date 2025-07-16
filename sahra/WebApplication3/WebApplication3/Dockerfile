   FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
   WORKDIR /app
   EXPOSE 80

   FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
   WORKDIR /src
   COPY . .
   RUN dotnet restore ./SirketApi.csproj
   RUN dotnet publish ./SirketApi.csproj -c Release -o /app/publish

   FROM base AS final
   WORKDIR /app
   COPY --from=build /app/publish .
   ENTRYPOINT ["dotnet", "SirketApi.dll"]