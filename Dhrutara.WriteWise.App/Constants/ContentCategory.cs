using System.ComponentModel.DataAnnotations;

namespace Dhrutara.WriteWise.App.Constants
{
    public enum ContentCategory
    {
        None = 0,
        Romantic = 1,
        Birthday = 2,
        Wedding = 3,
        [Display(Name = "Wedding anniversary")]
        WeddingAnniversary = 4,
        Condolence = 5,
        Promotion = 6,
        [Display(Name = "New Job")]
        NewJob = 7,
    }
}
