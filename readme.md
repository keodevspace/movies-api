# 🎬 Movie API - Estudo de GraphQL com Go
Este projeto é uma prática guiada para entender os fundamentos de pacotes, visibilidade e geração de código no Go usando o framework gqlgen.

## 🛠️ Passo a Passo da Construção

- Passo 1: Limpeza Total
Para garantir que não haja conflitos de pacotes antigos, limpe o ambiente:
````bash
Remove-Item -Recurse -Force graph, internal, server.go, go.mod, go.sum, gqlgen.yml
````

- Passo 2: Inicialização do Projeto
Defina o Namespace do projeto e baixe as dependências base:
````bash
go mod init github.com/keodevspace/movie-api
go get github.com/99designs/gqlgen
mkdir graph
mkdir internal
````

- Passo 3: O Contrato (Schema)
Crie o arquivo graph/schema.graphqls. Ele é a "única fonte da verdade" da API:
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

- Passo 4: O "Banco de Dados" In-Memory
Crie o arquivo internal/store.go.

Nota de Estudo: Usamos MovieList com "M" maiúsculo para que o campo seja exportado (público) para outros pacotes.
````Go
package internal

import "github.com/keodevspace/movie-api/graph/model"

type DataStore struct {
	MovieList []*model.Movie 
}

func NewStore() *DataStore {
	return &DataStore{
		MovieList: make([]*model.Movie, 0),
	}
}
````

- Passo 5: Configuração e Geração
Crie o gqlgen.yml na raiz:
````YAML
schema:
  - graph/*.graphqls
exec:
  filename: graph/generated.go
  package: graph
model:
  filename: graph/model/models_gen.go
  package: model
resolver:
  layout: follow-schema
  dir: graph
  package: graph
````
Rode o gerador para criar os modelos e o código de runtime:
````bash
go run github.com/99designs/gqlgen generate
````

- Passo 6: Resolvers (A Lógica)
Para evitar erros de "Duplicate Method", separamos a definição da implementação.

No arquivo graph/resolver.go (Apenas a estrutura):
````Go
package graph

import "github.com/keodevspace/movie-api/internal"

type Resolver struct {
    Store *internal.DataStore
}
No arquivo graph/schema.resolvers.go (A lógica):

Go
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

- Passo 7: O Servidor (Main)
Crie o server.go para ligar os pontos e injetar a dependência da Store no Resolver.

## 🧠 Desafio de Revisão
No arquivo server.go, fazemos a injeção assim:
Resolvers: &graph.Resolver{Store: myStore}

Pergunta: Se alterarmos o campo de Store para store (minúsculo) no arquivo resolver.go, o que acontece com a linha acima no server.go?
Resposta: O código não compilará, pois campos iniciados com letra minúscula são privados ao pacote original.

## O Fluxo da Informação
1 - O Cliente faz uma requisição.
2 - O server.go recebe e passa para o generated.go.
3 - O generated.go valida se a requisição está de acordo com o schema.graphqls.
4 - Se estiver OK, ele chama o método correspondente no schema.resolvers.go.
5 - O Resolver usa o que estiver guardado no resolver.go (o banco) para processar.
5 - O resultado volta o caminho todo até o cliente.
