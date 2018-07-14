using System.ComponentModel.DataAnnotations;

namespace TCOMSapps.Features.Account
{
    public class ExternalLoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
