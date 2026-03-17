// go.mod and go.sum files are used to manage dependencies in Go projects. The go.mod file specifies the module path and the required dependencies, while the go.sum file contains checksums for the dependencies to ensure integrity. In this case, the go.mod file indicates that the project is a module named "github.com/keodevspace/movie-api" and lists several dependencies with their respective versions.
// O Gerenciador: is like .csproj e o packages.config from .NET projects, but for Go. It defines the module and its dependencies. The go.mod file is essential for managing the project's dependencies and ensuring that the correct versions are used when building the project.
module github.com/keodevspace/movie-api

go 1.25.0

require (
	github.com/99designs/gqlgen v0.17.88 // indirect
	github.com/agnivade/levenshtein v1.2.1 // indirect
	github.com/goccy/go-yaml v1.19.2 // indirect
	github.com/google/uuid v1.6.0 // indirect
	github.com/sosodev/duration v1.4.0 // indirect
	github.com/urfave/cli/v3 v3.7.0 // indirect
	github.com/vektah/gqlparser/v2 v2.5.32 // indirect
	golang.org/x/mod v0.33.0 // indirect
	golang.org/x/sync v0.19.0 // indirect
	golang.org/x/text v0.34.0 // indirect
	golang.org/x/tools v0.42.0 // indirect
)
