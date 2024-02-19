FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "Gtw.GestorTarifas.Api/Gtw.GestorTarifas.Api.csproj" --configfile "NuGet.config" --disable-parallel
COPY . .
WORKDIR "/src/Gtw.GestorTarifas.Api"
RUN dotnet build "Gtw.GestorTarifas.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Gtw.GestorTarifas.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Gtw.GestorTarifas.Api.dll"]