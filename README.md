# Minha API com SQLite

API RESTful em ASP.NET Core para gerenciamento de produtos e categorias, usando Entity Framework Core e SQLite.

## Tecnologias

- ASP.NET Core 8
- Entity Framework Core
- SQLite
- Swagger

## Estrutura

```text
MinhaApiComSQLite/
  Controllers/
  Data/
  DTOs/
  Models/
  Repositories/
  Services/
  Migrations/
```

## Como executar

Entre na pasta do projeto:

```bash
cd MinhaApiComSQLite
```

Restaure as dependencias:

```bash
dotnet restore
```

Aplique as migrations:

```bash
dotnet ef database update
```

Execute a API:

```bash
dotnet run
```

A API ficara disponivel em:

- `http://localhost:5000`
- Swagger: `http://localhost:5000`

## Como executar os testes

Na pasta raiz da solution, execute:

```bash
dotnet test
```

Os testes automatizados validam regras de categoria, produto, paginacao, desconto progressivo e relatorio de estatisticas.

## Endpoints principais

### Categorias

Criar categoria:

```http
POST /api/categorias
Content-Type: application/json

{
  "nome": "Eletronicos"
}
```

Listar categorias:

```http
GET /api/categorias
```

Atualizar categoria:

```http
PUT /api/categorias/1
Content-Type: application/json

{
  "nome": "Informatica"
}
```

Excluir categoria:

```http
DELETE /api/categorias/1
```

### Produtos

Criar produto:

```http
POST /api/produtos
Content-Type: application/json

{
  "nome": "Produto Exemplo",
  "preco": 50.00,
  "categoriaId": 1
}
```

Listar produtos com paginacao:

```http
GET /api/produtos?pageNumber=1&pageSize=10
```

Buscar produto por id:

```http
GET /api/produtos/1
```

Atualizar produto:

```http
PUT /api/produtos/1
Content-Type: application/json

{
  "nome": "Produto Atualizado",
  "preco": 75.00,
  "categoriaId": 1
}
```

Excluir produto:

```http
DELETE /api/produtos/1
```

### Desconto progressivo

```http
POST /api/produtos/1/desconto
Content-Type: application/json

{
  "quantidade": 12
}
```

Regras:

- Quantidade maior que 5: 5% de desconto
- Quantidade maior que 10: 10% de desconto
- Quantidade maior que 20: 15% de desconto

### Relatorios

```http
GET /api/relatorios/estatisticas
```

Retorna:

- Total de produtos cadastrados
- Media de precos
- Valor total dos produtos cadastrados

## Validacoes e regras de negocio

- Produto deve ter nome obrigatorio, descritivo e unico.
- Categoria deve ter nome obrigatorio, descritivo e unico.
- Preco do produto deve ser maior que zero.
- Produto deve estar vinculado a uma categoria existente.
- Nome de produto e categoria e salvo em letras maiusculas.
- Listagem de produtos e ordenada por preco crescente.
- Produtos com nome contendo `PROMOCAO` recebem marcador `[Em Promocao]` na listagem.
