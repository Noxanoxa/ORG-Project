namespace Org.Domains.Shared;

public class Result
{
    public bool Successed { get; set; }
    public List<string> Errors { get; set; }
    public List<ErrorCode> ErrorCodes { get; set; }

    private Result(bool success, List<string> errors = null)
    {
        Successed = success;
        Errors = errors;
    }

    private Result(bool success, List<ErrorCode> errors = null)
    {
        Successed = success;
        ErrorCodes = errors;
    }

    public Result()
    {
        Successed = true;
    }

    public static Result Succes => new Result();

    public static Result Failure(List<string> errors) =>
        new Result(false, errors);

    public static Result Failure(List<ErrorCode> errors) =>
        new Result(false, errors);
}