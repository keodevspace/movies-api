package graph

// Este arquivo é onde a "mágica" acontece. No gqlen, as assinaturas das funções
// são geradas automaticamente, mas a implementação (a lógica) é nossa.
// Pense neles como os Action Methods de um Controller no ASP.NET WebAPI.

import (
	"context"
	"fmt"

	"github.com/keodevspace/movie-api/graph/model"
)

// AddMovie é o resolver para o campo 'addMovie' (Nossa Mutation / POST)
func (r *mutationResolver) AddMovie(ctx context.Context, input model.NewMovie) (*model.Movie, error) {
	// 1. Criamos a instância do novo filme (struct)
	// Usamos o fmt.Sprintf para gerar um ID dinâmico baseado no tamanho da lista.
	// No Go, o '&' cria um ponteiro para o objeto, evitando cópias desnecessárias de memória.
	newMovie := &model.Movie{
		ID:    fmt.Sprintf("M-%d", len(r.Store.MovieList)+1),
		Title: input.Title,
		Genre: input.Genre,
	}

	// 2. Acessamos o nosso "Armazém" (Store) que foi injetado no Resolver.
	// O 'append' no Go funciona assim: lista = append(lista, novoItem)
	// Diferente do C# (list.Add), o append retorna um novo slice atualizado.
	r.Store.MovieList = append(r.Store.MovieList, newMovie)

	// 3. Retornamos o ponteiro do filme criado.
	// O GraphQL vai transformar isso no JSON que você vê no navegador.
	return newMovie, nil
}

// Movies é o resolver para o campo 'movies' (Nossa Query / GET ALL)
func (r *queryResolver) Movies(ctx context.Context) ([]*model.Movie, error) {
	// No C# você faria: return Ok(_context.Movies.ToList());
	// No Go, apenas retornamos o slice (lista) diretamente do nosso Store.
	return r.Store.MovieList, nil
}

// --- PONTE DE CONEXÃO (NÃO ALTERAR) ---
// Estas funções e structs abaixo são o "esqueleto" que o gqlgen usa para
// conectar o seu Resolver base às operações de Query e Mutation.

// Mutation retorna a implementação do MutationResolver.
func (r *Resolver) Mutation() MutationResolver { return &mutationResolver{r} }

// Query retorna a implementação do QueryResolver.
func (r *Resolver) Query() QueryResolver { return &queryResolver{r} }

// Aqui definimos os tipos que "embutem" o Resolver principal.
// Isso permite que o 'r.Store' funcione dentro das funções acima.
type (
	mutationResolver struct{ *Resolver }
	queryResolver    struct{ *Resolver }
)
