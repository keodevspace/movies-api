// Here are saved changes to the resolver.go file, which contains the implementation of the resolver functions for the GraphQL API. The resolver functions are responsible for fetching and manipulating data based on the GraphQL schema defined in the project. In this case, we have implemented a resolver for the addMovie mutation, which allows clients to add new movies to the store.
// O Armazém - Like Constrollerswhere I receive the data and manipulate it, in this case, I have a function that adds a new movie to the store. The resolver takes the input from the GraphQL mutation, creates a new movie object, and appends it to the list of movies in the store. Finally, it returns the newly created movie object as a response to the client. (IDataContext)
package graph

// This import "context" is necessary for the resolver functions to handle request contexts, which can include deadlines, cancellation signals, and other request-scoped values.
// The "fmt" package is used for formatting strings, which is helpful for generating unique IDs for new movies.
// The "github.com/keodevspace/movie-api/graph/model" import is necessary to access the data models defined for the GraphQL schema, such as Movie and NewMovie.
import (
	"context"
	"fmt"

	"github.com/keodevspace/movie-api/graph/model"
)

// This func is a resolver for the addMovie mutation. It creates a new movie based on the input and adds it to the store.
func (r *mutationResolver) AddMovie(ctx context.Context, input model.NewMovie) (*model.Movie, error) {
	movie := &model.Movie{
		ID:    fmt.Sprintf("M%d", len(r.Store.Movies)+1),
		Title: input.Title,
		Genre: input.Genre,
	}
	// Add the new movie to the store's movie list
	r.Store.MovieList = append(r.Store.MovieList, movie)
	return movie, nil
}
