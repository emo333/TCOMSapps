using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace TCOMSapps.Features.OOSTitles.entities.DTOs
{
    [NotMapped]
    public class TransferDto : Transfer
    {
        [NotMapped]
        [DisplayName("Location")]
        public string LocationName { get; set; }
    }
}