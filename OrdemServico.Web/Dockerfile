# Estágio 1: Build (precisa do SDK completo para buildar o React)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Restaura dependências do .NET
COPY OrdemServico.sln .
COPY OrdemServico/OrdemServico.csproj OrdemServico/
COPY OrdemServico.Web/OrdemServico.Web.csproj OrdemServico.Web/
COPY Bussiness/Bussiness.csproj Bussiness/
COPY Domain/Domain.csproj Domain/
COPY Infra/Infra.csproj Infra/
# Copia o package.json para otimizar o cache do npm
COPY OrdemServico.Web/ClientApp/package.json OrdemServico.Web/ClientApp/
RUN dotnet restore OrdemServico.sln

# Copia o resto do código
COPY . .

# Publica o projeto Web
# O 'dotnet publish' para projetos com React automaticamente
# roda o 'npm install' e 'npm run build'
WORKDIR "/src/OrdemServico.Web"
RUN dotnet publish "OrdemServico.Web.csproj" -c Release -o /app/publish

# Estágio 2: Imagem Final
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Adiciona usuário não-root
RUN useradd -m -s /bin/bash appuser
USER appuser

EXPOSE 8080
ENTRYPOINT ["dotnet", "OrdemServico.Web.dll"]