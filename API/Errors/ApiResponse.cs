
namespace API.Errors;

public class ApiResponse
{
    public ApiResponse(int statusCode, string messsage = null)
    {
        StatusCode = statusCode;
        Messsage = messsage ?? GetDefaultMessageForStatusCode(statusCode);
    }


    public int StatusCode { get; set; }
    public string Messsage { get; set; }
    private string GetDefaultMessageForStatusCode(int statusCode)
    {
        return statusCode switch
        {
            400 => "A bad request you have made",
            401 => "Authorized, You are not",
            404 => "Resourse found, It was not",
            500 => "Server error",
            _ => null

        };
    }
}
