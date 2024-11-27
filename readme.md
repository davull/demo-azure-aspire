[aspire-samples](https://github.com/dotnet/aspire-samples/tree/main/samples/AspireWithJavaScript)

# Setup

[Azure Aspire Templates](https://learn.microsoft.com/en-us/dotnet/aspire/fundamentals/aspire-sdk-templates)

```powershell

# Install Azure cli and Azure Developer cli
choco install azure-cli
choco install azd

# Install aspire templates
dotnet new install Aspire.ProjectTemplates

# List templates
dotnet new list aspire
```

```powershell
# Trust development certificate
dotnet dev-certs https --trust
```

## Azure

```powershell
# Check azure cli and azure developer cli version
az --version
azd version

# Login to azure
az login

# List subscriptions
az account list --output Table --all
```

# Project

```powershell
# Create a new solution
dotnet new aspire-starter -n AspireStarterDemo

# Build application
dotnet build .\AspireStarterDemo.sln

# Run application
dotnet run --project .\AspireStarterDemo.AppHost\AspireStarterDemo.AppHost.csproj
```

# Deploy

[Deploy to Azure](https://learn.microsoft.com/en-us/dotnet/aspire/deployment/azure/aca-deployment)

## azd deploy

```powershell

# Initialize
azd init
> Use code in the current directory

# Login
azd auth login

# Provision infrastructure
azd provision

# Deploy application
azd deploy

# Destroy infrastructure
azd down

```

## azd infra synth

[Generate Bicep from .NET Aspire project model](https://learn.microsoft.com/en-us/dotnet/aspire/deployment/azure/aca-deployment-azd-in-depth?branch=main&tabs=windows#generate-bicep-from-net-aspire-app-model)

```powershell
# Enable alpha feature
azd config set alpha.infraSynth on

# Create bicep file
azd infra synth --force
```

## Pipeline

[Azure Pipeline](https://github.com/Azure-Samples/azd-starter-bicep/blob/main/.azdo/pipelines/azure-dev.yml)

```powershell
azd pipeline config
```
