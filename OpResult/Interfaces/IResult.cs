namespace OpResult.Interfaces;

public interface IResult
{
    bool IsSuccess { get; }
    bool IsFailure { get; }
    string? ErrorMessage { get; }
    string? SuccessMessage { get; }
    List<string>? ErrorsMessages { get; }

    void ThrowIfFailure();
    string ToString();
}
