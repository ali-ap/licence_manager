FROM mcr.microsoft.com/dotnet/core/sdk:3.1  AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY Licence.Service.Common/Licence.Service.Common.csproj Licence.Service.Common/
RUN dotnet restore Licence.Service.Common/Licence.Service.Common.csproj
COPY Licence.Service.Registration/Licence.Service.Registration.csproj Licence.Service.Registration/
RUN dotnet restore Licence.Service.Registration/Licence.Service.Registration.csproj

COPY . .
RUN dotnet build Licence.Service.Registration/Licence.Service.Registration.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Licence.Service.Registration/Licence.Service.Registration.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Licence.Service.Registration/Licence.Service.Registration.dll"]
