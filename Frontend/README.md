# React + TypeScript + Vite

This template provides a minimal setup to get React working in Vite with HMR and some ESLint rules.

Currently, two official plugins are available:

- [@vitejs/plugin-react](https://github.com/vitejs/vite-plugin-react/blob/main/packages/plugin-react) uses [Oxc](https://oxc.rs)
- [@vitejs/plugin-react-swc](https://github.com/vitejs/vite-plugin-react/blob/main/packages/plugin-react-swc) uses [SWC](https://swc.rs/)

## React Compiler

The React Compiler is not enabled on this template because of its impact on dev & build performances. To add it, see [this documentation](https://react.dev/learn/react-compiler/installation).

## Expanding the ESLint configuration

If you are developing a production application, we recommend updating the configuration to enable type-aware lint rules:

```js
export default defineConfig([
  globalIgnores(['dist']),
  {
    files: ['**/*.{ts,tsx}'],
    # Nexo Frontend

    Interface React e TypeScript para consultar os serviços de juros, comissões e movimentação de estoque da API DesafioTarget.

    ## Requisitos

    - Node.js 20.19 ou superior (ou 22.12 ou superior)
    - npm
    - API backend em execução

    ## Iniciar em desenvolvimento

    No terminal, na pasta do projeto:

    ```bash
    npm install
    npm run dev
    ```

    Abra o endereço informado pelo Vite no terminal. Normalmente é `http://localhost:5173`. Se essa porta estiver ocupada, o Vite informa outra porta.

    ## Configurar a API

    O frontend usa `http://localhost:5271/api` como endereço base, definido em `src/services/api.ts`. Inicie o backend nessa porta antes de enviar chamadas. Se a API estiver em outra URL, atualize o `baseURL` nesse arquivo.

    O backend também precisa permitir requisições CORS vindas da origem do frontend, normalmente `http://localhost:5173`.

    Se aparecer `ERR_CONNECTION_REFUSED`, confirme que o backend está em execução e que a URL/porta configurada está correta. Esse erro acontece quando não há servidor aceitando a conexão no endereço informado.

    ## Usar o painel

    O menu agrupa as operações por área:

    - **Financeiro / Calcular juros:** informe valor original e data de vencimento. Envia `GET /Interest/calculate`.
    - **Comissões / Consultar resultados:** busca os resultados existentes com `GET /Commissions/calculate`.
    - **Comissões / Enviar vendas:** adicione uma ou mais vendas. Envia `POST /Commissions/calculate` com este formato:

      ```json
      {
        "vendas": [
          { "vendedor": "Ana", "valor": 125.50 }
        ]
      }
      ```

    - **Estoque / Registrar movimentação:** informe produto, quantidade, tipo e descrição. O combo mostra “Entrada” e “Saída”, enviando `Entry` ou `Exit` em `POST /StockMovements`.

    As respostas e erros das chamadas aparecem no painel ao lado do formulário.

    ## Comandos

    ```bash
    npm run dev      # inicia o servidor de desenvolvimento
    npm run build    # verifica os tipos e gera a versão de produção em dist/
    npm run lint     # executa o ESLint
    npm run preview  # serve localmente a versão já compilada
    ```

    
