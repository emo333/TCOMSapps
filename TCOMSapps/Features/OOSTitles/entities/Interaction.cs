using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TCOMSapps.Features.OOSTitles.entities
{
    public class Interaction
    {

        public int Id { get; set; }

        public int TitleId { get; set; }

        [DisplayName("Interaction Date")]
        public DateTime InteractionDt { get; set; } = DateTime.Now;

        [DisplayName("Employee")]
        public string InteractionUserId { get; set; }

        [DisplayName("Type")]
        public string InteractionType { get; set; }
        public string Notes { get; set; }
        public DateTime? FollowUpDt { get; set; }
        public string FollowUpNotes { get; set; }
        public int VehYr { get; set; }
        public string VehMake { get; set; }
        public string VehModel { get; set; }
        public string Vin { get; set; }
        public string TitleState { get; set; }
        public bool IsNew { get; set; }
        public DateTime? TitleIssuedDt { get; set; }
        public DateTime? ReceivedDt { get; set; }
        public string CustFName { get; set; }
        public string CustLName { get; set; }
        public string CustAddr1 { get; set; }
        public string CustAddr2 { get; set; }
        public string CustCity { get; set; }
        public string CustState { get; set; }
        public string CustZip { get; set; }

        [Required]
        [DisplayName("Phone Number")]
        public string CustPhone { get; set; }

        [DisplayName("Other Phone Number")]
        public string CustPhone2 { get; set; }

        [DisplayName("Email")]
        public string CustEmail { get; set; }
        public string CustFName2 { get; set; }
        public string CustLName2 { get; set; }
        public string CustFName3 { get; set; }
        public string CustLName3 { get; set; }
        public TitleReceivedFromTypes TitleRecievedFromType { get; set; }//1=Dealer 2=Lienholder
        public string LhDlrName { get; set; }
        public string LhDlrAddr1 { get; set; }
        public string LhDlrAddr2 { get; set; }
        public string LhDlrCity { get; set; }
        public string LhDlrState { get; set; }
        public string LhDlrZip { get; set; }

        public enum TitleReceivedFromTypes
        {
            Dealer = 1,
            Lienholder = 2
        }

        public enum TitleReceivedMethod
        {
            Mail = 1,
            InPerson = 2
        }
    }
}