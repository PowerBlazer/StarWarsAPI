namespace StarWarsAPI.Common;

public class AuthorizationResult
{
    public AuthorizationResult()
    {
        Errors = new List<string>();
        IsSuccess = true;
    }

    public bool IsSuccess { get; protected set; }
    public IList<string> Errors { get; } 
    public string? Token { get; set; }

    public void Failed(params string[] errors)
    {
        IsSuccess = false;

        foreach (var error in errors)
        {
            Errors.Add(error);
        }
    }
}