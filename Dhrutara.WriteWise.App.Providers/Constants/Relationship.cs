using System.ComponentModel.DataAnnotations;

namespace Dhrutara.WriteWise.App.Providers.Constants
{
    public enum Relationship
    {
        None = 0,
        Mother = 1,
        Father = 2,
        Wife = 3,
        Husband = 4,
        Son = 5,
        Daughter = 6,
        Sister = 7,
        Brother = 8,
        [Display(Name = "Significant Other")]
        SignificantOther = 9,
        Uncle = 10,
        Aunt = 11,
        Friend = 12,
        Colleague = 13,
        Girlfriend = 14,
        Boyfriend = 15
    }
}
