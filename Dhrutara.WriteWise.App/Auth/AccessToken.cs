namespace Dhrutara.WriteWise.App.Auth
{
    internal class AccessToken
    {
        private const long MIN_SECONDS_BEFORE_EXPIRES = 5;
        public string? AuthToken { get; set; }
        public DateTimeOffset? ExpiresOn { get; set; }

        public bool IsValid()
        {
            if (string.IsNullOrWhiteSpace(AuthToken) || !ExpiresOn.HasValue)
            {
                return false;
            }

            long ticksBeforeExpires = (ExpiresOn-DateTimeOffset.UtcNow).Value.Ticks;

            TimeSpan difference = new(ticksBeforeExpires);

            return difference.TotalSeconds >= 5;
        }
    }
}
