namespace Dhrutara.WriteWise.App.Auth
{
    internal class UserContext
    {
        AccessToken _accesToken;

        public UserContext()
        {
            _accesToken = new AccessToken();
        }
        public string? AccountIdentifer { get; set; }
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public string? GivenName { get; set; }
        public string? FamilyName { get; set; }
        public string? Email { get; set; }
        public string? AccessToken
        {
            get
            {
                return _accesToken.AuthToken;
            }
            set
            {
                _accesToken.AuthToken = value;
            }
        }
        public DateTimeOffset? ExpiresOn
        {
            get
            {
                return _accesToken.ExpiresOn;
            }
            set
            {
                _accesToken.ExpiresOn = value;
            }
        }

        public bool IsAccessTokenValid()
        {
            return _accesToken.IsValid();
        }
    }
}
