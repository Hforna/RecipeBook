FROM mcr.microsoft.com/dotnet/sdk:8.0 as build-env

WORKDIR /app

COPY  src/ .

WORKDIR /app/backend/ProjectAspNet

RUN dotnet restore
RUN dotnet publish -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/aspnet:8.0

WORKDIR /app

COPY --from=build-env /opt .

ENTRYPOINT [ "dotnet", "ProjectAspNet.dll" ]