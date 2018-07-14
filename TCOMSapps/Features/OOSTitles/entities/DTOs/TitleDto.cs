using System.ComponentModel.DataAnnotations.Schema;

namespace TCOMSapps.Features.OOSTitles.entities.DTOs
{
    [NotMapped]
    public class TitleDto : Title
    {
        [NotMapped]
        public bool Over30 { get; set; }

        [NotMapped]
        public bool Warning { get; set; }

        [NotMapped]
        public bool RequestedNotRouted { get; set; }

        [NotMapped]
        public bool RoutedNotReceived { get; set; }

    }
}