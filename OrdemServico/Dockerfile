# Estágio 1: Build (com SDK)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia arquivos de projeto e restaura dependências
COPY OrdemServico.sln .
COPY OrdemServico/OrdemServico.csproj OrdemServico/
COPY OrdemServico.Web/OrdemServico.Web.csproj OrdemServico.Web/
COPY Bussiness/Bussiness.csproj Bussiness/
COPY Domain/Domain.csproj Domain/
COPY Infra/Infra.csproj Infra/
RUN dotnet restore OrdemServico.sln

# Copia todo o resto e publica a API
COPY . .
WORKDIR "/src/OrdemServico"
RUN dotnet publish "OrdemServico.csproj" -c Release -o /app/publish

# Estágio 2: Imagem Final (Runtime)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Adiciona usuário não-root por segurança
RUN useradd -m -s /bin/bash appuser
USER appuser

EXPOSE 8080
ENTRYPOINT ["dotnet", "OrdemServico.dll"]