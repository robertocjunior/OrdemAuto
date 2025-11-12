# ===============================
# 🏗️ STAGE 1: Build do Front-End (React)
# ===============================
FROM node:20 AS frontend-build
WORKDIR /src

# Copia package.json e instala dependências
COPY OrdemServico.Web/ClientApp/package*.json ./ClientApp/
WORKDIR /src/ClientApp
RUN npm install

# Copia o restante e faz build
COPY OrdemServico.Web/ClientApp ./
RUN npm run build --omit=dev || true  # ignora erro do "move"
RUN cp -r build /src/wwwroot

# ===============================
# ⚙️ STAGE 2: Build do Back-End (.NET)
# ===============================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia arquivos de projeto e restaura dependências
COPY OrdemServico.sln .
COPY Bussiness/Bussiness.csproj Bussiness/
COPY Domain/Domain.csproj Domain/
COPY Infra/Infra.csproj Infra/
COPY OrdemServico/OrdemServico.csproj OrdemServico/
COPY OrdemServico.Web/OrdemServico.Web.csproj OrdemServico.Web/
RUN dotnet restore OrdemServico.sln

# Copia o resto do código e publica
COPY . .
WORKDIR /src/OrdemServico.Web
RUN dotnet publish -c Release -o /app/publish

# ===============================
# 🚀 STAGE 3: Runtime
# ===============================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Copia publicação do .NET
COPY --from=build /app/publish .

# Copia build do React para o wwwroot da aplicação ASP.NET
COPY --from=frontend-build /src/ClientApp/build ./wwwroot

# Segurança: usa usuário sem privilégios
RUN useradd -m -s /bin/bash appuser
USER appuser

EXPOSE 8080
ENTRYPOINT ["dotnet", "OrdemServico.Web.dll"]
