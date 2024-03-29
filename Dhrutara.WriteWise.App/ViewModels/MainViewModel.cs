﻿namespace Dhrutara.WriteWise.App.ViewModels
{
    public partial class MainViewModel : BaseViewModel
    {
        public MainViewModel()
        {
            newContentOptions ??= new ContentOptions
            {
                Category = ContentCategory.None,
                Type = ContentType.Joke,
                Receiver = Relationship.None
            };
        }

        [ObservableProperty]
        public string welcomeMessage = string.Empty;

        [ObservableProperty]
        public string message = "Tap here to get started";

        [ObservableProperty]
        public ContentOptions newContentOptions = new()
        {
            Category = ContentCategory.None,
            Type = ContentType.Message,
            Receiver = Relationship.None
        };

        [ObservableProperty]
        public bool enableContentRefresh = false;
    }
}