// This file contains the main entry point for the Movie API server. It sets up the GraphQL server using gqlgen and starts an HTTP server to handle incoming requests.
// O Maestro: is like the Program.cs in C#. It is the main entry point of the application where the server is configured and started. In this file, we create a new store for our movie data, set up the GraphQL server with our schema and resolvers, and start an HTTP server to listen for incoming requests on port 8080. The playground handler is also set up to provide a web interface for testing our GraphQL API.
// Here I create a new store instance using the internal package, which will hold our movie data. Then, I create a new GraphQL server using the gqlgen handler, passing in the executable schema and resolvers. Finally, I set up HTTP handlers for the root path (which serves the GraphQL playground) and the /query path (which serves the GraphQL API), and start the server on port 8080.

package main

import (
	"log"
	"net/http"

	"github.com/99designs/gqlgen/graphql/handler"
	"github.com/99designs/gqlgen/graphql/playground"
	"github.com/keodevspace/movie-api/graph"
	"github.com/keodevspace/movie-api/internal"
)

func main() {
	myStore := internal.NewStore()
	// Create a new GraphQL server with the executable schema and resolvers, passing in the store for data access.
	srv := handler.NewDefaultServer(graph.NewExecutableSchema(graph.Config{Resolvers: &graph.Resolver{Store: myStore}}))

	// Set up HTTP handlers for the root path (serving the GraphQL playground) and the /query path (serving the GraphQL API).
	http.Handle("/", playground.Handler("Movie API Playground", "/query"))
	http.Handle("/query", srv)

	// Start the HTTP server on port 8080 and log a message indicating that the server is running.
	log.Printf("Movie API server is running at http://localhost:8080/")
	log.Fatal(http.ListenAndServe(":8080", nil))
}
