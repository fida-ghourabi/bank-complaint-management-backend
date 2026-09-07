# ==========================
# Stage 1 : Build
# ==========================

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /src


COPY ["BankComplaintManagement.sln", "."]


COPY ["src/BankComplaintManagement.API/BankComplaintManagement.API.csproj", "src/BankComplaintManagement.API/"]

COPY ["src/BankComplaintManagement.Application/BankComplaintManagement.Application.csproj", "src/BankComplaintManagement.Application/"]

COPY ["src/BankComplaintManagement.Domain/BankComplaintManagement.Domain.csproj", "src/BankComplaintManagement.Domain/"]

COPY ["src/BankComplaintManagement.Infrastructure/BankComplaintManagement.Infrastructure.csproj", "src/BankComplaintManagement.Infrastructure/"]

COPY ["tests/BankComplaintManagement.Application.Tests/BankComplaintManagement.Application.Tests.csproj", "tests/BankComplaintManagement.Application.Tests/"]

COPY ["tests/BankComplaintManagement.IntegrationTests/BankComplaintManagement.IntegrationTests.csproj", "tests/BankComplaintManagement.IntegrationTests/"]

RUN dotnet restore


COPY . .


WORKDIR "/src/src/BankComplaintManagement.API"


RUN dotnet publish \
    -c Release \
    -o /app/publish \
    --no-restore



# ==========================
# Stage 2 : Runtime
# ==========================

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final


WORKDIR /app


COPY --from=build /app/publish .

# Run the application as a non-root user
USER app

EXPOSE 8080


ENTRYPOINT ["dotnet", "BankComplaintManagement.API.dll"]
