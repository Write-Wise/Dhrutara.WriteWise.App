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
        [Display(Name = "Death condolence")]
        DeathCondolence = 5,
        Promotion = 6,
        NewJob = 7,
    }
}
