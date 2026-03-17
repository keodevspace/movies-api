// Like the Repository pattern, the Store pattern is a way to abstract away the data access layer of an application. It provides a simple interface for interacting with the underlying data storage, allowing you to easily swap out different implementations (e.g., in-memory, database, etc.) without affecting the rest of your application. In this example, we have a DataStore struct that contains a list of movies. The NewStore function initializes the DataStore with an empty list of movies. You can add methods to the DataStore struct to perform operations such as adding, retrieving, updating, or deleting movies from the list.
// Persistência
package internal

import "github.com/keodevspace/movie-api/graphql/model"

type DataStore struct {
	MovieList []*model.Movie
}

func NewStore() *DataStore {
	return &DataStore{
		MovieList: make([]*model.Movie, 0),
	}
}
