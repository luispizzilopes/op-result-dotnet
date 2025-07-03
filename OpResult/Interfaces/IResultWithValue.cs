namespace OpResult.Interfaces;

public interface IResult<T> : IResult
{
    T? Value { get; }
}
