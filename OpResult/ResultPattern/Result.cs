using OpResult.Exception;
using OpResult.Interfaces;
using System.Diagnostics;
using System.Text.Json.Serialization;

namespace OpResult.ResultPattern;

public class Result : IResult
{
    public Guid TraceId { get; }
    public bool IsSuccess { get; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ErrorMessage { get; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? SuccessMessage { get; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<string>? ErrorsMessages { get; }

    public bool IsFailure => !IsSuccess; 

    protected Result(bool isSuccess, string? errorMessage = null, string? successMessage = null, List<string>? errorsMessages = null)
    {
        TraceId = Guid.NewGuid(); 
        IsSuccess = isSuccess;
        ErrorMessage = errorMessage;
        SuccessMessage = successMessage;
        ErrorsMessages = errorsMessages; 
    }

    public static Result Success(string successMessage) => new(true, successMessage: successMessage);

    public static Result Failure(string errorMessage) => new(false, errorMessage: errorMessage);

    public static Result ManyErrors(List<string> errors) => new(false, errorsMessages: errors);

    public void ThrowIfFailure()
    {
        if (IsFailure)
            throw new ResultException(GetErrorOrErrors());
    }

    public override string ToString() =>
        IsSuccess
            ? $"Success: {SuccessMessage}"
            : $"Failure: {GetErrorOrErrors()}";

    public string GetErrorOrErrors()
    {
        return (ErrorMessage ?? (ErrorsMessages != null ? string.Join(", ", ErrorsMessages) : string.Empty)); 
    }
}
