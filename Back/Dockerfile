FROM mcr.microsoft.com/dotnet/sdk:6.0 AS dev
WORKDIR /app

COPY ["Back/Back.csproj", "Back/"]
RUN dotnet restore "Back/Back.csproj"

COPY ["Back", "Back/"]

WORKDIR "/app"

ENTRYPOINT ["dotnet", "watch", "run", "--urls=http://+:7035"] 