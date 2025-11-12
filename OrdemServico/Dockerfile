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

# --- CORREÇÃO: Limpa o cache do NuGet e instala o EF ---
# Isso corrige o erro de "pacote corrompido"
RUN dotnet nuget locals all --clear && \
    dotnet tool install --global dotnet-ef
# --- FIM DA CORREÇÃO ---

# Estágio 2: Imagem Final (Runtime)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Adiciona usuário não-root por segurança
RUN useradd -m -s /bin/bash appuser

# --- Copia as ferramentas do estágio de build ---
USER root
COPY --from=build /root/.dotnet/tools /home/appuser/.dotnet/tools
RUN chown -R appuser:appuser /home/appuser/.dotnet

# Volta para o usuário appuser
USER appuser

# Adiciona o diretório de ferramentas ao PATH do appuser
ENV PATH="$PATH:/home/appuser/.dotnet/tools"

EXPOSE 8080
ENTRYPOINT ["dotnet", "OrdemServico.dll"]