using MVCTask.Infrastructure.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCTask.Infrastructure.ResultPattern;
public class Result
{
    public bool IsSuccess { get; }
    public string ErrorMessage { get; }
    public ErrorType ErrorType { get; }

    protected Result(bool isSuccess, string errorMessage, ErrorType errorType)
    {
        IsSuccess = isSuccess;
        ErrorMessage = errorMessage;
        ErrorType = errorType;
    }

    public static Result Success() => new Result(true, null, ErrorType.None);

    public static Result Failure(string errorMessage) => new Result(false, errorMessage, ErrorType.NormalError);

    public static Result InternalFailure(string errorMessage) => new Result(false, errorMessage, ErrorType.InternalServerError);
}

public class Result<T> : Result
{
    public T Value { get; }

    private Result(bool isSuccess, T value, string errorMessage, ErrorType errorType)
        : base(isSuccess, errorMessage, errorType)
    {
        Value = value;
    }

    public static Result<T> Success(T value) => new Result<T>(true, value, null, ErrorType.None);

    public static new Result<T> Failure(string errorMessage) => new Result<T>(false, default!, errorMessage, ErrorType.NormalError);

    public static new Result<T> InternalFailure(string errorMessage) => new Result<T>(false, default!, errorMessage, ErrorType.InternalServerError);
}