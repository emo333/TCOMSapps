using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TCOMSapps.Features.OOSTitles.entities.DTOs;

namespace TCOMSapps.Features.OOSTitles.entities
{
    public class Title
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("VIN")]
        public string Vin { get; set; }

        [Required]
        [DisplayName("Title State")]
        public string TitleState { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0000}")]
        [DisplayName("Year")]
        public int VehYr { get; set; }

        [Required]
        [DisplayName("Make")]
        public string VehMake { get; set; }

        [Required]
        [DisplayName("Model")]
        public string VehModel { get; set; }

        [DisplayName("New Vehicle")]
        public bool NewVeh { get; set; }

        [DisplayName("Issued Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? TitleIssueDt { get; set; } //TODO: not used

        public DateTime EntryDt { get; set; } = DateTime.Now;

        [DisplayName("Input User")]
        public string UserId { get; set; }

        [DisplayName("Received Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = false)]
        public DateTime RecDt { get; set; } = DateTime.Now;

        [DisplayName("Method Received")]
        public TitleReceivedMethod RecMethod { get; set; } = TitleReceivedMethod.Mail;

        [DisplayName("Initial Location")]
        public int InitialLocation { get; set; }

        [Required]
        [DisplayName("First Name")]
        public string CustFName { get; set; }

        [Required]
        [DisplayName("Last Name")]
        public string CustLName { get; set; }

        [Required]
        [DisplayName("Address Line 1")]
        public string CustAddr1 { get; set; }

        [DisplayName("Address Line 2")]
        public string CustAddr2 { get; set; }

        [Required]
        [DisplayName("City")]
        public string CustCity { get; set; }

        [Required]
        [DisplayName("State")]
        public string CustState { get; set; }

        [Required]
        [DisplayName("Zip Code")]
        public string CustZip { get; set; }

        [DisplayName("Phone Number")]
        public string CustPhone { get; set; }

        [DisplayName("Other Number")]
        public string CustPhone2 { get; set; }

        [DisplayName("Email")]
        public string CustEmail { get; set; }

        [DisplayName("2nd First Name")]
        public string CustFName2 { get; set; }

        [DisplayName("2nd Last Name")]
        public string CustLName2 { get; set; }

        [DisplayName("3rd First Name")]
        public string CustFName3 { get; set; }

        [DisplayName("3rd Last Name")]
        public string CustLName3 { get; set; }

        [DisplayName("Sent to FLHSMV")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? SentToDmvDt { get; set; }

        [DisplayName("Returned to sender")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? SentBackToLhDlrDt { get; set; }

        public string SentUserId { get; set; }

        [DisplayName("From Dealer/Lienholder")]
        public TitleReceivedFromTypes TitleRecievedFromType { get; set; } = TitleReceivedFromTypes.Lienholder;//1=Dealer 2=Lienholder

        public int CountyId { get; set; }

        [DisplayName("Notes")]
        public string TitleNotes { get; set; }

        [DisplayName("Follow Up")]
        public bool FollowUp { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedDt { get; set; }

        public string DeletedBy { get; set; }

        public List<Interaction> Interactions { get; set; }

        public int LastInteraction { get; set; }

        public List<Transfer> Transfers { get; set; }

        public int LastTransfer { get; set; }

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


        public TitleDto MapToTitleDto(Title title)
        {
            var titleDto = new TitleDto
            {
                Id = title.Id,
                Vin = title.Vin,
                TitleState = title.TitleState,
                VehYr = title.VehYr,
                VehMake = title.VehMake,
                VehModel = title.VehModel,
                NewVeh = title.NewVeh,
                TitleIssueDt = title.TitleIssueDt,
                EntryDt = title.EntryDt,
                UserId = title.UserId,
                RecDt = title.RecDt,
                RecMethod = title.RecMethod,
                InitialLocation = title.InitialLocation,
                CustFName = title.CustFName,
                CustLName = title.CustLName,
                CustAddr1 = title.CustAddr1,
                CustAddr2 = title.CustAddr2,
                CustCity = title.CustCity,
                CustState = title.CustState,
                CustZip = title.CustZip,
                CustPhone = title.CustPhone,
                CustPhone2 = title.CustPhone2,
                CustEmail = title.CustEmail,
                CustFName2 = title.CustFName2,
                CustLName2 = title.CustLName2,
                CustFName3 = title.CustFName3,
                CustLName3 = title.CustLName3,
                SentToDmvDt = title.SentToDmvDt,
                SentBackToLhDlrDt = title.SentBackToLhDlrDt,
                TitleRecievedFromType = title.TitleRecievedFromType,
                SentUserId = title.SentUserId,
                CountyId = title.CountyId,
                FollowUp = title.FollowUp,
                IsDeleted = title.IsDeleted,
                Interactions = title.Interactions,
                Transfers = title.Transfers
            };

            return titleDto;
        }

    }
}