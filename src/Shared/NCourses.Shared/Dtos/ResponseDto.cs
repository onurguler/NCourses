using System.Net;
using System.Text.Json.Serialization;

namespace NCourses.Shared.Dtos;

public class ResponseDto<T>
{
    public T Data { get; private set; } = default!;

    [JsonIgnore]
    public HttpStatusCode StatusCode { get; private set; }

    [JsonIgnore]
    public bool IsSuccessful { get; private set; }

    public List<string>? Errors { get; private set; }

    public static ResponseDto<T> Success(HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        return Success(default!, statusCode);
    }

    public static ResponseDto<T> Success(T data, HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        return new ResponseDto<T>() { Data = data, StatusCode = statusCode, IsSuccessful = true };
    }

    public static ResponseDto<T> Fail(List<string> errors, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
    {
        return new ResponseDto<T>() { Errors = errors, StatusCode = statusCode, IsSuccessful = false };
    }

    public static ResponseDto<T> Fail(string error, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
    {
        return Fail(new List<string>() { error }, statusCode);
    }
}