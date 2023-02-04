
namespace WriteWiseApp.Auth
{
    public static class Constants
    {
        public static readonly string ClientId = "6ce9ca32-ac01-4ab2-bdab-bd010e0d0e39";
        public static readonly string[] Scopes = new string[] { "openid", "offline_access" };
        public static readonly string TenantName = "writewiseb2c";
        public static readonly string TenantId = "writewise.dhrutara.net";
        public static readonly string SignInPolicy = "b2c_1_signup_signin";
        public static readonly string AuthorityBase = $"https://{TenantName}.b2clogin.com/tfp/{TenantId}/";
        public static readonly string AuthoritySignIn = $"{AuthorityBase}{SignInPolicy}";

    }
}
