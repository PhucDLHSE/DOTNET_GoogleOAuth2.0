namespace DotnetGoogleOAuth2.Services
{
    public interface IRoleResolver
    {
        string ResolveRole(string email);
    }
}
