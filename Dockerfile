FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

COPY . .

RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS final
WORKDIR /app
COPY --from=build /app/out .

# Adicione a linha abaixo para instalar o utilitário "wait-for-it"
RUN apt-get update && apt-get install -y wait-for-it

ENTRYPOINT ["dotnet", "ERP.Api.dll"]
