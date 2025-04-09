
using MVCTask.Infrastructure.Enum;
using MVCTask.Infrastructure.ResultPattern;

namespace MVCTask.Infrastructure.Helper;
public static class ResultHelper
{
    public static Result ErrorHandler(Result result)
    {
        return result.ErrorType switch
        {
            ErrorType.NormalError => Result.Failure(result.ErrorMessage),
            ErrorType.InternalServerError => Result.InternalFailure(result.ErrorMessage),
            _ => Result.Success()
        };
    }

    public static Result<T> ErrorHandler<T>(Result<T> result)
    {
        return result.ErrorType switch
        {
            ErrorType.NormalError => Result<T>.Failure(result.ErrorMessage),
            ErrorType.InternalServerError => Result<T>.InternalFailure(result.ErrorMessage),
            _ => Result<T>.Success(result.Value)
        };
    }
}
