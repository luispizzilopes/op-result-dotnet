using OpResult.Interfaces;
using OpResult.ResultPattern;

public static class ResultExtensions
{
    public static async Task<IResult<T>> Then<T>(
        this Task<Result> task,
        Func<Task<ResultWithValue<T>>> next)
    {
        var result = await task;
        return result.IsSuccess
            ? await next()
            : result.ErrorsMessages is not null
                ? ResultWithValue<T>.ManyErrors(result.ErrorsMessages)
                : ResultWithValue<T>.Failure(result.ErrorMessage ?? string.Empty);
    }

    public static async Task<IResult<TOut>> Then<TIn, TOut>(
        this Task<ResultWithValue<TIn>> task,
        Func<TIn, Task<ResultWithValue<TOut>>> next)
    {
        var result = await task;
        return result.IsSuccess && result.Value is not null
            ? await next(result.Value)
            : result.ErrorsMessages is not null
                ? ResultWithValue<TOut>.ManyErrors(result.ErrorsMessages)
                : ResultWithValue<TOut>.Failure(result.ErrorMessage ?? string.Empty);
    }

    public static async Task<IResult> Then<T>(
        this Task<ResultWithValue<T>> task,
        Func<T, Task<Result>> next)
    {
        var result = await task;
        return result.IsSuccess && result.Value is not null
            ? await next(result.Value)
            : result.ErrorsMessages is not null
                ? Result.ManyErrors(result.ErrorsMessages)
                : Result.Failure(result.ErrorMessage ?? string.Empty);
    }

    public static async Task<IResult> Then(
        this Task<Result> task,
        Func<Task<Result>> next)
    {
        var result = await task;
        return result.IsSuccess
            ? await next()
            : result.ErrorsMessages is not null
                ? Result.ManyErrors(result.ErrorsMessages)
                : Result.Failure(result.ErrorMessage ?? string.Empty);
    }

    public static async Task<IResult<TOut>> Then<TOut>(
        this Task<ResultWithValue<TOut>> task,
        Func<Task<ResultWithValue<TOut>>> next)
    {
        var result = await task;
        return result.IsSuccess
            ? await next()
            : result.ErrorsMessages is not null
                ? ResultWithValue<TOut>.ManyErrors(result.ErrorsMessages)
                : ResultWithValue<TOut>.Failure(result.ErrorMessage ?? string.Empty);
    }

    public static async Task<IResult<TOut>> Then<TIn, TOut>(
        this Task<ResultWithValue<TIn>> task,
        Func<TIn, Task<IResult<TOut>>> next)
    {
        var result = await task;
        return result.IsSuccess && result.Value is not null
            ? await next(result.Value)
            : result.ErrorsMessages is not null
                ? ResultWithValue<TOut>.ManyErrors(result.ErrorsMessages)
                : ResultWithValue<TOut>.Failure(result.ErrorMessage ?? string.Empty);
    }

    public static async Task<IResult> Then(
        this Task<Result> task,
        Func<Task<IResult>> next)
    {
        var result = await task;
        return result.IsSuccess
            ? await next()
            : result.ErrorsMessages is not null
                ? Result.ManyErrors(result.ErrorsMessages)
                : Result.Failure(result.ErrorMessage ?? string.Empty);
    }
}
