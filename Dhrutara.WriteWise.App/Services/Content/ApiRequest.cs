
namespace Dhrutara.WriteWise.App.Services.Content
{
    public class ApiRequest
    {
        public ContentCategory Category { get; set; }

        public ContentType Type { get; set; }

        public Relationship? To { get; set; }
    }
}
