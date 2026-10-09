# DesafioTarget

Este README descreve como executar o projeto localmente e como empacotá-lo/executá-lo com Docker.

## Sumário
- Pré-requisitos
- Executar localmente
- Usar Docker
- Exemplo de Dockerfile e docker-compose

## Pré-requisitos
- .NET 8 SDK instalado: https://dotnet.microsoft.com/
- (Opcional) Docker instalado: https://www.docker.com/

## Executar localmente
1. Abra um terminal na pasta do projeto (onde está a solução .slnx).
2. Restaurar pacotes e compilar:

   dotnet restore
   dotnet build --configuration Debug

3. Executar o projeto (ajuste o caminho do projeto se necessário):

   dotnet run --project DesafioTarget

4. Acesse a aplicação no endereço exibido no terminal (geralmente http://localhost:5000 ou https://localhost:5001 para apps web).

## Executar com Docker

Observação: se já existir um Dockerfile no repositório, use-o. Caso não exista, abaixo há um exemplo de Dockerfile multi-stage para aplicações .NET.

### Exemplo de Dockerfile (adicionar na raiz do projeto)

```dockerfile
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "DesafioTarget/DesafioTarget.slnx"
RUN dotnet publish "DesafioTarget" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish ./
ENTRYPOINT ["dotnet", "DesafioTarget.dll"]
```

### Construir a imagem Docker

Na raiz do repositório:

  docker build -t desafio-target:latest -f Dockerfile .

Se o Dockerfile estiver dentro de DesafioTarget, ajuste o comando:

  docker build -t desafio-target:latest -f DesafioTarget/Dockerfile .

### Executar o contêiner

  docker run --rm -p 5000:80 --name desafio-target desafio-target:latest

- Mapeie portas conforme necessário. No exemplo acima, a porta 80 do container é mapeada para 5000 da máquina host.

## Exemplo docker-compose.yml

```yaml
version: '3.8'
services:
  desafio:
	image: desafio-target:latest
	build:
	  context: .
	  dockerfile: DesafioTarget/Dockerfile # ajuste se necessário
	ports:
	  - "5000:80"
	environment:
	  - ASPNETCORE_ENVIRONMENT=Production
```

Usar:

  docker-compose up --build

## Variáveis de ambiente e configuração
- Configure conexões de banco, chaves e outras variáveis via environment variables ou appsettings.{Environment}.json.
- Ao executar com Docker, defina variáveis no docker run (-e KEY=value) ou no docker-compose.

## Testes
Se existirem projetos de teste na solução, execute:

  dotnet test