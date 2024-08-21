namespace MoviesApi.Models;

public class ApiResponse<T>
    {
    public bool Success { get; set; } 
    public T Data { get; set; }
    public string Message { get; set; }

    public ApiResponse(bool success, T data, string message)
        {
        Success = success;
        Data = data;
        Message = message;
        }
    }

// Generic Type Parameter: T is a placeholder for a type that will be specified when the ApiResponse class is instantiated. T can be any type, such as int, string, Movie, or even another user-defined class.

