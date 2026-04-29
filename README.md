📌 Objetivo do Projeto

Este projeto consiste na criação de uma **API RESTful** em **C# com ASP.NET Core e SQLite**, .O objetivo é aplicar boas práticas de desenvolvimento para garantir eficiência, segurança.

🔧 Tecnologias Utilizadas

- **ASP.NET Core** (Desenvolvimento da API)
- **Entity Framework Core** (ORM para persistência de dados)
- **SQLite** (Banco de dados leve para armazenamento)
- **HttpClient** (Consumo de API no cliente WinForms)

📂 Estrutura do Projeto(Opcional, caso fizer será um diferencial para o teste)

```
📁 MinhaApiComSQLite
│── 📁 Controllers
│── 📁 Services
│── 📁 Repositories
│── 📁 DTOs
│── 📁 Models
│── 📁 Data (Contexto do banco de dados)
│── Program.cs
│── Startup.cs
```

🚀 Como Executar o Projeto

1️⃣ Use o Template e Clone o Repositório

2️⃣ Configurando o Banco de Dados

- O banco de dados **SQLite** já está configurado no projeto.
- Para aplicar as migrações, execute:

```bash
dotnet ef database update
```
A API estará disponível em `http://localhost:5000`.

📌 Funcionalidades Implementadas

API

✅ CRUD de Produtos e Categorias\
✅ Paginação de produtos\ 

📜 Exemplo de Requisição

Criar Produto (POST)

```json
POST /api/produtos
{
  "nome": "Produto Exemplo",
  "preco": 50.00,
  "categoriaId": 1
}
```

Neste teste, você deverá desenvolver uma API RESTful em C# com ASP.NET Core e 
SQLite, aplicando boas práticas de arquitetura e desenvolvimento para garantir 
eficiência, segurança e manutenibilidade. 

**1. Requisitos Funcionais** 
  - Implementar os métodos CRUD para a entidade Produto, com os seguintes 
  atributos: 
      - Id (auto gerado pelo banco de dados) 
      - Nome (string, deve ser descritivo e único) 
      - Preço (decimal, maior que zero) 
      - CategoriaId (relacionamento com a entidade Categoria)
        
  - Implementar os métodos CRUD para a entidade Categoria, com os seguintes 
  atributos: 
      - Id (auto gerado pelo banco de dados) 
      - o Nome (string, deve ser descritivo e único)

  - Implementar paginação para a listagem de produtos.

  (Opcional, caso fizer será um diferencial para o teste)
  - Implementar uma lógica de desconto progressivo, onde:
    o Se a quantidade comprada for maior que 5, aplicar 5% de desconto.
    o Se for maior que 10, aplicar 10% de desconto.
    o Se for maior que 20, aplicar 15% de desconto.

  - Criar um endpoint que retorne relatórios e estatísticas, como: 
      - Total de produtos cadastrados 
      - Média de preços dos produtos 
      - Valor total dos produtos no estoque
        
  - Aplicar validações rigorosas na entrada de dados.

**2. Requisitos Técnicos**
    - Utilizar ASP.NET Core para desenvolver a API. 
    - Utilizar Entity Framework Core com SQLite para persistência de dados. 
    - Aplicar arquitetura em camadas separadas (Controllers, Services, Repositories, 
DTOs)(Opcional, caso fizer será um diferencial para o teste). 
    
**3. Regras de Negócio Avançadas** 
    - O nome do produto deve ser armazenado sempre com a primeira letra 
    maiúscula. 
    - O preço do produto não pode ser negativo ou igual a zero. 

**4. Instruções Gerais**
   - Criar uma documentação mínima explicando como rodar o projeto e exemplos de 
    requisições. 
   - Enviar um link para o repositório atualizado.
       
   - Paginação 
      A paginação permite que grandes volumes de dados sejam retornados de forma eficiente, 
      evitando sobrecarregar o banco de dados e melhorando a experiência do usuário. 
      Exemplo de implementação no ASP.NET Core.
      Chamando o endpoint: GET /api/produtos?pageNumber=1&pageSize=10

  4.1 O nome do produto, caso seja informado em letras minúsculas, deve ser convertido para letras maiúsculas ao ser inserido no banco.

  4.2 Obter Produtos (GET)
  Objetivo: Implementar o método para listar todos os produtos disponíveis.
  
  Instruções:
  
  Recupere todos os produtos armazenados na base de dados.
  
  Laço de Repetição:
  
  Percorra a lista de produtos e aplique um filtro de exemplo (por exemplo, se o nome do produto contiver a palavra "promoção", adicione um marcador no nome do produto como "[Em Promoção]").
  Ordenação de Lista:

  Ordene os produtos pelo preço em ordem crescente (do mais barato para o mais caro).

  4.3 Atualizar Produto (PUT)
  Objetivo: Implementar o método para atualizar os dados de um produto existente.
  
  Validação:
  
  Verifique se o produto existe no banco de dados. Caso contrário, retorne um erro 404 - Not Found.
  O preço não pode ser menor que zero.


  4.4 Excluir Produto (DELETE)
  Objetivo: Implementar o método para excluir um produto da base de dados.
  
  Instruções:
  
  O produto será identificado pelo Id.
  
  Verifique se o produto com o Id fornecido existe. Caso contrário, retorne um erro 404 - Not Found.
  
  Exclusão de Produto:
  
  Após excluir o produto, retorne um status 204 - No Content.
  
  4.5 Validação de Dados em Criar e Atualizar Produto
  Objetivo: Adicionar validação de dados para garantir que as informações enviadas são válidas.
  
  Instruções:
  
  O nome do produto deve ser uma string não vazia. Caso contrário, o método deve retornar um erro 400 - Bad Request.
  
  O preço do produto deve ser maior que zero. Caso contrário, o método deve retornar um erro 400 - Bad Request.

  Exemplo de Validação:
  
  Nome vazio: "nome": "" → Retornar 400 - Bad Request.
  Preço negativo: "preco": -10.00 → Retornar 400 - Bad Request.
  Exemplo de Resposta de Erro:
  
  json
  {
    "error": "O preço deve ser maior que zero."
  }

✅ Critérios de Avaliação

- Implementação correta dos requisitos funcionais e técnicos.
- Uso de boas práticas de código e arquitetura.
- Documentação clara e objetiva.

Pedimos que realizem a entrega do teste dentro do prazo estabelecido, mesmo que a implementação não esteja totalmente concluída.
Todo o conteúdo desenvolvido será considerado na avaliação, levando em conta a organização, as boas práticas adotadas e a abordagem técnica aplicada.

---

✉️ **Dúvidas? Entre em contato!**

