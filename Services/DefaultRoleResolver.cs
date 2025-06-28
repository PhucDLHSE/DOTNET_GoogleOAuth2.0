namespace DotnetGoogleOAuth2.Services
{
    public class DefaultRoleResolver : IRoleResolver
    {
        private readonly string[] _adminWhitelist;

        public DefaultRoleResolver(string[] adminWhitelist)
        {
            _adminWhitelist = adminWhitelist;
        }

        public string ResolveRole(string email)
        {
            if (_adminWhitelist.Contains(email.ToLower()))
                return "admin";
            if (email.EndsWith("@fpt.edu.vn", StringComparison.OrdinalIgnoreCase))
                return "lecturer";
            return "student";
        }
    }
}
