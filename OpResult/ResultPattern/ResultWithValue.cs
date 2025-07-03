using OpResult.Interfaces;

namespace OpResult.ResultPattern;

public class ResultWithValue<T> : Result, IResult<T>
{
    public T? Value { get; }

    private ResultWithValue(
        bool isSuccess, 
        T? value, 
        string? errorMessage = null, 
        string? successMessage = null, 
        List<string>? errorsMessages = null
    )
        : base(isSuccess, errorMessage, successMessage, errorsMessages)
    {
        Value = value;
    }

    public static ResultWithValue<T> Success(T value, string successMessage) => new ResultWithValue<T>(true, value, successMessage: successMessage);

    public static ResultWithValue<T> Failure(string errorMessage) => new ResultWithValue<T>(false, default, errorMessage: errorMessage);

    public static ResultWithValue<T> ManyErrors(List<string> errors) => new ResultWithValue<T>(false, default, errorsMessages: errors);

    public override string ToString() =>
        IsSuccess
            ? $"Success: {SuccessMessage}"
            : $"Failure: {GetErrorOrErrors()}";
}
