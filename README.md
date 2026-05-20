# PIM-III
PIM 3°semestre

# Guia de Instalação e Dependências: Luxx Motors API

Para compilar, executar e testar o projeto de forma local, o ambiente de desenvolvimento precisa ser configurado com ferramentas globais do sistema e com pacotes específicos do ecossistema .NET Core (NuGet).

---

## 1. Requisitos Globais do Sistema

Antes de baixar os pacotes do projeto, certifique-se de que sua máquina possui as seguintes ferramentas instaladas e configuradas no terminal:

* **.NET SDK (Versão 7.0 ou 8.0):** Kit de desenvolvimento oficial necessário para compilar o código C#, restaurar pacotes e gerenciar o ciclo de vida da Web API.
* **Docker Desktop:** Essencial para criar e rodar a instância isolada do contêiner do **Microsoft SQL Server**, garantindo que o banco de dados funcione com a mesma configuração do ambiente de produção.

---

## 2. Pacotes NuGet (Dependências do Back-end)

Estes pacotes realizam o mapeamento Objeto-Relacional (ORM) e estabelecem a conectividade entre os controladores C# e as tabelas físicas do SQL Server.

Para baixar e instalar as dependências, abra o terminal do seu sistema operacional **dentro da pasta raiz do seu projeto** (onde está localizado o arquivo `.csproj`) e execute os comandos abaixo em sequência:

bash
# Instala o provedor de dados oficial do Entity Framework Core para o SQL Server
dotnet add package Microsoft.EntityFrameworkCore.SqlServer

# Instala o núcleo do mecanismo ORM do Entity Framework Core
dotnet add package Microsoft.EntityFrameworkCore

# Adiciona as ferramentas de Design necessárias para compilar e gerar Migrations
dotnet add package Microsoft.EntityFrameworkCore.Design

Ferramenta de CLI Adicional (Altamente Recomendável)
Para gerenciar atualizações na estrutura das tabelas ou rodar comandos do banco de dados diretamente pelo terminal via linha de comando, instale a ferramenta global do Entity Framework

dotnet tool install --global dotnet-ef

3. Dependências do Front-end (Sem Necessidade de Download)
A interface gráfica do usuário (camada cliente) foi arquitetada para ser leve e stateless. Toda a biblioteca de componentes e os recursos de acessibilidade foram vinculados através de CDNs (Content Delivery Networks) externos integrados diretamente nas tags <link> e <script> dos arquivos HTML.

Portanto, não é necessário realizar downloads locais (como npm, Node.js ou yarn). Ao abrir o site, o navegador resolverá automaticamente as seguintes dependências:

Tailwind CSS: Framework utilitário de estilização responsiva carregado via CDN.

Font Awesome Icons: Biblioteca de vetores e ícones gráficos carregada via CDN.

Plugin VLibras: Componente oficial de acessibilidade digital do Governo Federal para tradução automática do conteúdo em Libras.

4. Passos Rápidos para Execução
Com as dependências instaladas, execute os comandos abaixo no terminal para inicializar o sistema:

# 1. Garanta que o seu contêiner do SQL Server esteja ativo no Docker
# 2. Restaura e valida todas as dependências baixadas
dotnet restore

# 3. Limpa arquivos de compilações antigas em cache
dotnet clean

# 4. Compila o projeto e inicia o servidor local da API
dotnet run
