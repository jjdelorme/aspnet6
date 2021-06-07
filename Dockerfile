FROM mcr.microsoft.com/dotnet/nightly/sdk:6.0
WORKDIR /app
COPY ./MinApi /source
RUN dotnet publish /source/MinApi.csproj -o /app

ENTRYPOINT ["./MinApi"]