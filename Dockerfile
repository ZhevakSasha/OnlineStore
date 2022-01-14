FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app 


COPY OnlineStore.ServiceApi/*.csproj ./OnlineStore.ServiceApi/
COPY OnlineStore.DataAccess/*.csproj ./OnlineStore.DataAccess/
COPY OnlineStore.BusinessLogic/*.csproj ./OnlineStore.BusinessLogic/

RUN dotnet restore "OnlineStore.ServiceApi/OnlineStore.ServiceApi.csproj"


COPY OnlineStore.DataAccess/. ./OnlineStore.DataAccess
COPY OnlineStore.BusinessLogic/. ./OnlineStore.BusinessLogic
COPY OnlineStore.ServiceApi/. ./OnlineStore.ServiceApi

RUN dotnet publish "OnlineStore.ServiceApi/OnlineStore.ServiceApi.csproj" -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app

COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "OnlineStore.ServiceApi.dll"]