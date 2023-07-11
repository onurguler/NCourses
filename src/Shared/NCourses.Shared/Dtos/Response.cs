using System.Net;
using System.Text.Json.Serialization;

namespace NCourses.Shared.Dtos;

public class Response<T>
{
    public T Data { get; private set; } = default!;

    [JsonIgnore]
    public HttpStatusCode StatusCode { get; private set; }

    [JsonIgnore]
    public bool IsSuccessful { get; private set; }

    public List<string>? Errors { get; private set; }

    public static Response<T> Success(HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        return Success(default!, statusCode);
    }

    public static Response<T> Success(T data, HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        return new Response<T>() { Data = data, StatusCode = statusCode, IsSuccessful = true };
    }

    public static Response<T> Fail(List<string> errors, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
    {
        return new Response<T>() { Errors = errors, StatusCode = statusCode, IsSuccessful = false };
    }

    public static Response<T> Fail(string error, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
    {
        return Fail(new List<string>() { error }, statusCode);
    }
}