
# Environnement de développement .NET 10
FROM mcr.microsoft.com/dotnet/sdk:10.0

WORKDIR /app

# Port de l'application
EXPOSE 8080

# Lancement en mode développement avec surveillance du code
ENTRYPOINT ["dotnet", "watch", "run", "--urls", "http://0.0.0.0:8080"]
