using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ES_TP.Data.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            
        }

        [Required]
        public double WorkingHours { get; set; }
    }
}