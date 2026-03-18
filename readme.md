# 🎬 Movie API - Estudo de GraphQL com Go

Este projeto é uma prática guiada desenvolvida para consolidar fundamentos de **Engenharia de Software** no ecossistema Go. O foco está na aplicação de conceitos como **visibilidade de pacotes (encapsulamento)**, **Injeção de Dependência (DI)** e o fluxo de **geração de código** utilizando o framework `gqlgen`.

---

## 🏗️ Arquitetura e Passo a Passo

### Passo 1: Inicialização e Gerenciamento de Dependências
Diferente do .NET, onde o NuGet lida com abstrações complexas, no Go o `go mod` é direto. Iniciamos o namespace e estruturamos os diretórios para separar a lógica de transporte (GraphQL) da lógica de domínio (Internal).

````bash
# Limpeza para evitar conflitos de pacotes antigos
Remove-Item -Recurse -Force graph, internal, server.go, go.mod, go.sum, gqlgen.yml

# Inicialização
go mod init [github.com/keodevspace/movie-api](https://github.com/keodevspace/movie-api)
go get [github.com/99designs/gqlgen](https://github.com/99designs/gqlgen)
mkdir graph
mkdir internal
````

### Passo 2: Design Schema-First (O Contrato)
A API é definida pelo contrato. O arquivo graph/schema.graphqls é a Single Source of Truth (Única Fonte da Verdade). Aqui definimos os tipos, queries e mutations que o gerador de código usará como base.

````GraphQL
type Movie {
  id: ID!
  title: String!
  genre: String!
}

input NewMovie {
  title: String!
  genre: String!
}

type Query {
  movies: [Movie!]!
}

type Mutation {
  addMovie(input: NewMovie!): Movie!
}
````

### Passo 3: Persistência In-Memory e Encapsulamento
Criamos o internal/store.go. Note que em Go, a capitalização define a visibilidade (Público/Privado). <br>

Utilizamos um campo exportado (MovieList) para permitir que pacotes externos interajam com o estado da aplicação, simulando um repositório.

````Go
package internal

import "[github.com/keodevspace/movie-api/graph/model](https://github.com/keodevspace/movie-api/graph/model)"

type DataStore struct {
  MovieList []*model.Movie
}

func NewStore() *DataStore {
  return &DataStore{
    MovieList: make([]*model.Movie, 0),
  }
}
````

### Passo 4: Automação e Geração de Código
O arquivo gqlgen.yml mapeia como o código gerado deve se comportar. Ao rodar o gerador, o Go cria o generated.go (runtime) e os modelos necessários, garantindo type-safety total.
````bash
# Rode o gerador para criar os modelos e o código de runtime
go run [github.com/99designs/gqlgen](https://github.com/99designs/gqlgen) generate
````

### Passo 5: Implementação dos Resolvers (Lógica de Negócio)
Para manter o princípio da responsabilidade única, separamos a estrutura do Resolver da implementação dos métodos:
- Resolver.go: Define a estrutura e as dependências (Injeção da Store). <br>
- Schema.resolvers.go: Contém a implementação real das Queries e Mutations.
````Go
func (r *mutationResolver) AddMovie(ctx context.Context, input model.NewMovie) (*model.Movie, error) {
    newMovie := &model.Movie{
        ID:    fmt.Sprintf("M-%d", len(r.Store.MovieList)+1),
        Title: input.Title,
        Genre: input.Genre,
    }
    r.Store.MovieList = append(r.Store.MovieList, newMovie)
    return newMovie, nil
}

func (r *queryResolver) Movies(ctx context.Context) ([]*model.Movie, error) {
    return r.Store.MovieList, nil
}
````

### Passo 6: O Servidor (Main)
Crie o server.go para ligar os pontos e realizar a injeção de dependência da Store no Resolver. <br>

---

### 🧠 Desafio de Arquitetura (Review)
Ao configurar o server.go, realizamos a injeção assim:<br>
Resolvers: &graph.Resolver{Store: myStore}

Pergunta de Design: Se alterarmos o campo Store para store (minúsculo) no arquivo resolver.go, o que acontece?<br>

Resposta Técnica: O código apresentará erro de compilação. Em Go, identificadores iniciados com letra minúscula são privados ao pacote. Isso reforça o controle de acesso e protege a integridade dos dados entre diferentes camadas da aplicação.


### 🔄 Fluxo da Informação
- Transporte: O server.go recebe o payload JSON via HTTP e entrega para o gqlgen.
- Marshalling: O generated.go valida se a requisição está de acordo com o schema.graphqls.
- Execution: Se estiver OK, ele chama o método correspondente no schema.resolvers.go.
- Data: O Resolver usa a Store (o "banco" na memória RAM) para processar a lógica.
- Response: O resultado volta o caminho todo até o cliente com o dado tipado.


### 🧪Testando a API
Adicionar Filme (Mutation):
````GraphQL
mutation {
  addMovie(input: {
    title: "Rocky Balboa",
    genre: "Drama/Esporte"
  }) {
    id
    title
    genre
  }
}
````
Listar Filmes (Query):
````GraphQL
query {
  movies {
    id
    title
    genre
  }
}
````
Exemplo de Resposta:
````JSON
{
  "data": {
    "movies": [
      {
        "id": "M-1",
        "title": "Rocky Balboa",
        "genre": "Drama/Esporte"
      }
    ]
  }
}
````

----
Roadmap: Próxima evolução focará em Middlewares para logs e Autenticação JWT.