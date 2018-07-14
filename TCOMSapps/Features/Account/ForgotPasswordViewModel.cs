using System.ComponentModel.DataAnnotations;

namespace TCOMSapps.Features.Account
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
