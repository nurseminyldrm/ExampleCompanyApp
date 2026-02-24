namespace ExampleCompanyApp.Api.AuthenticationService
{
    public interface IJwtService
    {
        string GenerateToken(string email, string role);
    }
}
