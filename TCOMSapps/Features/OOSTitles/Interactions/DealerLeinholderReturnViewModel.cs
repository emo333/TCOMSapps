using Microsoft.AspNetCore.Identity;
using System;
using System.Security.Claims;
using TCOMSapps.Data;
using TCOMSapps.Features.OOSTitles.entities;

namespace TCOMSapps.Features.OOSTitles.Interactions
{
    public class DealerLeinholderReturnViewModel
    {
        public Interaction Interaction { get; set; }
        public Title Title { get; set; }
        public ClaimsPrincipal User { get; set; }


        public Interaction CreateNewInteractionFromTitle(Title title, UserManager<ApplicationUser> userManager)
        {

            var interaction = new Interaction
            {
                TitleId = title.Id,
                Vin = title.Vin,
                TitleState = title.TitleState,
                VehYr = title.VehYr,
                VehMake = title.VehMake,
                VehModel = title.VehModel,
                IsNew = title.NewVeh,
                TitleIssuedDt = title.TitleIssueDt,
                InteractionDt = DateTime.Now,
                InteractionUserId = userManager.GetUserAsync(User).Result.Id.ToString(),
                ReceivedDt = title.RecDt,
                CustFName = title.CustFName,
                CustLName = title.CustLName,
                CustAddr1 = title.CustAddr1,
                CustAddr2 = title.CustAddr2,
                CustCity = title.CustCity,
                CustState = title.CustState,
                CustZip = title.CustZip,
                CustPhone = title.CustPhone,
                CustPhone2 = title.CustPhone2,
                CustFName2 = title.CustFName2,
                CustLName2 = title.CustLName2,
                CustFName3 = title.CustFName3,
                CustLName3 = title.CustLName3,
                InteractionType = "Title Return Letter",
                TitleRecievedFromType = (Interaction.TitleReceivedFromTypes)title.TitleRecievedFromType
            };
            return interaction;
        }

    }



}
