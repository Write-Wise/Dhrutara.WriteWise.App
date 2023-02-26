namespace Dhrutara.WriteWise.App.Models
{
    public class Configuration
    {
        public Settings? Settings { get; set; }
    }

    public class Settings
    {
        public Uri? ContentApiUri { get; set; }
    }
}
