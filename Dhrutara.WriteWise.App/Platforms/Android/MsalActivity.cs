using Android.App;
using Android.Content;
using Microsoft.Identity.Client;

namespace Dhrutara.WriteWise.App.Platforms.Android
{
    [Activity(Exported = true)]
    [IntentFilter(new[] { Intent.ActionView },
            Categories = new[] { Intent.CategoryBrowsable, Intent.CategoryDefault },
            DataHost = "auth",
            DataScheme = "msal6ce9ca32-ac01-4ab2-bdab-bd010e0d0e39")]
    public class MsalActivity : BrowserTabActivity
    {
    }
}
