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
