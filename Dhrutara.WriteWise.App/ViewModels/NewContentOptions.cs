namespace Dhrutara.WriteWise.App.ViewModels
{
    public partial class NewContentOptions 
    {
        public ContentType Type { get; set; }
        
        public ContentCategory Category { get; set; }

        public Relationship Receiver { get; set; }
    }
}
