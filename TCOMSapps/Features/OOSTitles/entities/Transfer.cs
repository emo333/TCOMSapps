using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TCOMSapps.Features.OOSTitles.entities
{
    public class Transfer
    {
        public int Id { get; set; }

        public int TitleId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = false)]
        [DisplayName("Requested On")]
        public DateTime? TransferRequestedDt { get; set; } = DateTime.Now;

        [DisplayName("Requested By")]
        public string TransferRequestUserId { get; set; }

        public int LocationId { get; set; }

        public string Method { get; set; }

        public string Notes { get; set; }

        [DisplayName("Received")]
        public bool IsReceived { get; set; } = false;

        [DisplayName("Received By")]
        public string ReceivedByUserId { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = false)]
        [DisplayName("Received On")]
        public DateTime? ReceivedDt { get; set; }

        [DisplayName("Sent")]
        public bool IsInRoute { get; set; } = false;

        [DisplayName("Sent By")]
        public string InRouteByUserId { get; set; }


        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = false)]
        [DisplayName("Sent On")]
        public DateTime? InRouteDt { get; set; }
    }
}