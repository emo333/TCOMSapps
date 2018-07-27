using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rotativa.AspNetCore;
using System;
using System.Net.Mail;
using System.Threading.Tasks;
using TCOMSapps.Data;
using TCOMSapps.Features.OOSTitles.entities;

namespace TCOMSapps.Features.OOSTitles.Interactions
{
  [Authorize]
  [Route("/OOSTitles/Interactions")]
  public class InteractionsController : Controller
  {
    private readonly ApplicationDbContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    public InteractionsController(ApplicationDbContext db,
                                  UserManager<ApplicationUser> userManager)
    {
      _db = db;
      _userManager = userManager;
    }


    [Route("TitleReturn")]
    public ActionResult DealerLeinholderReturn(int id)
    {
      var title = _db.Titles.Find(id);
      var vm = new DealerLeinholderReturnViewModel()
      {
        Title = title,
        User = User,
      };
      vm.Interaction = vm.CreateNewInteractionFromTitle(title, _userManager);
      return View(vm);
    }
    [HttpPost]
    [Route("TitleReturn")]
    public ActionResult DealerLeinholderReturn(DealerLeinholderReturnViewModel vm)
    {
      if (vm.Interaction.CustPhone == null) vm.Interaction.CustPhone = "N/A";
      _db.Interactions.Add(vm.Interaction);
      var tit = _db.Titles.Find(vm.Interaction.TitleId);
      tit.SentBackToLhDlrDt = DateTime.Now;
      _db.Entry(tit).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction(
          "SendTitleReturnLetterAsPdf",
          "Interactions",
          new { id = vm.Interaction.Id });
    }



    [Route("Notes")]
    public ActionResult Notes(int? id)
    {
      if (id == null) { return new StatusCodeResult(400); }
      var notesViewModel = new NotesViewModel(id, _db, _userManager);
      return View(notesViewModel);
    }
    [HttpPost]
    [Route("Notes")]
    [ValidateAntiForgeryToken]
    public ActionResult Notes(NotesViewModel notesViewModel)
    {
      if (notesViewModel == null)
      {
        throw new ArgumentNullException(nameof(notesViewModel));
      }
      if (!ModelState.IsValid) return View(notesViewModel);
      _db.Entry(notesViewModel.Interaction).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Details", "Titles", new { id = notesViewModel.Interaction.TitleId });
    }



    [Route("PhoneCall")]
    public ActionResult PhoneCall(int id)
    {
      var vm = new PhoneCallViewModel();
      var title = _db.Titles.Find(id);
      const string interactionType = "Phone Call";
      var inter = new Interaction()
      {
        TitleId = title.Id,
        InteractionDt = DateTime.Now,
        InteractionUserId = _userManager.GetUserAsync(User).Result.Id.ToString(),
        InteractionType = interactionType,
        VehYr = title.VehYr,
        VehMake = title.VehMake,
        VehModel = title.VehModel,
        Vin = title.Vin,
        TitleState = title.TitleState,
        IsNew = title.NewVeh,
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
        TitleRecievedFromType = (Interaction.TitleReceivedFromTypes)title.TitleRecievedFromType
      };
      vm.Interaction = inter;
      return View(vm);
    }
    [HttpPost]
    [Route("PhoneCall")]
    [ValidateAntiForgeryToken]
    public ActionResult PhoneCall(PhoneCallViewModel vm)
    {
      if (!ModelState.IsValid) return View(vm);
      var title = _db.Titles.Find(vm.Interaction.TitleId);
      const string interactionType = "Phone Call";
      var inter = new Interaction()
      {
        TitleId = title.Id,
        InteractionDt = DateTime.Now,
        InteractionUserId = _userManager.GetUserAsync(User).Result.Id.ToString(),
        InteractionType = interactionType,
        VehYr = title.VehYr,
        VehMake = title.VehMake,
        VehModel = title.VehModel,
        Vin = title.Vin,
        TitleState = title.TitleState,
        IsNew = title.NewVeh,
        CustFName = title.CustFName,
        CustLName = title.CustLName,
        CustAddr1 = title.CustAddr1,
        CustAddr2 = title.CustAddr2,
        CustCity = title.CustCity,
        CustState = title.CustState,
        CustZip = title.CustZip,
        CustPhone = vm.Interaction.CustPhone,
        CustPhone2 = vm.Interaction.CustPhone2,
        CustFName2 = title.CustFName2,
        CustLName2 = title.CustLName2,
        CustFName3 = title.CustFName3,
        CustLName3 = title.CustLName3,
        Notes = vm.Interaction.Notes,
        TitleRecievedFromType = (Interaction.TitleReceivedFromTypes)title.TitleRecievedFromType
      };
      _db.Interactions.Add(inter);
      _db.SaveChanges();
      title.LastInteraction = inter.Id;
      _db.Entry(title).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Details", "Titles", new { id = vm.Interaction.TitleId });
    }




    [Route("SendCustomerLetterAsPdf")]
    public async Task<IActionResult> SendCustomerLetterAsPdf(int id)
    {
      var vm = new InteractionViewModel();
      vm.Title = _db.Titles.Find(id);
      var viewName = "InitialNotificationLetterAsPdf";
      var interactionType = "Initial Notification Letter";
      ////todo: check for all required Title fields for letter
      if (await _db.Interactions.AnyAsync(i => i.TitleId == id && i.InteractionType == "Initial Notification Letter"))
      {  //check for prior notification letter sent
        interactionType = "Warning Letter";
        viewName = "WarningLetterAsPdf";
      }

      var inter = new Interaction()
      {
        TitleId = vm.Title.Id,
        InteractionDt = DateTime.Now,
        InteractionUserId = _userManager.GetUserAsync(User).Result.Id.ToString(),
        InteractionType = interactionType,
        VehYr = vm.Title.VehYr,
        VehMake = vm.Title.VehMake,
        VehModel = vm.Title.VehModel,
        Vin = vm.Title.Vin,
        TitleState = vm.Title.TitleState,
        IsNew = vm.Title.NewVeh,
        CustFName = vm.Title.CustFName,
        CustLName = vm.Title.CustLName,
        CustAddr1 = vm.Title.CustAddr1,
        CustAddr2 = vm.Title.CustAddr2,
        CustCity = vm.Title.CustCity,
        CustState = vm.Title.CustState,
        CustZip = vm.Title.CustZip,
        CustPhone = "PHONE",
        CustPhone2 = vm.Title.CustPhone2,
        CustFName2 = vm.Title.CustFName2,
        CustLName2 = vm.Title.CustLName2,
        CustFName3 = vm.Title.CustFName3,
        CustLName3 = vm.Title.CustLName3,
        ReceivedDt = vm.Title.RecDt,
        TitleRecievedFromType = (Interaction.TitleReceivedFromTypes)vm.Title.TitleRecievedFromType
      };
      _db.Interactions.Add(inter);
      _db.SaveChanges();
      vm.Title.LastInteraction = inter.Id;
      vm.County = await _db.Counties.FirstOrDefaultAsync();
      vm.Location = await _db.Locations.FindAsync(vm.Title.InitialLocation);
      _db.Entry(vm.Title).State = EntityState.Modified;
      _db.SaveChanges();
      return new ViewAsPdf(viewName, vm);
    }
    [Route("ViewCustomerLetterAsPdf")]
    public async Task<IActionResult> ViewCustomerLetterAsPdf(int id)
    {
      var vm = new InteractionViewModel();
      vm.Interaction = await _db.Interactions.FindAsync(id);
      vm.County = await _db.Counties.FirstOrDefaultAsync();
      var viewName = "WarningLetterAsPdfHistory";
      if (vm.Interaction.InteractionType == "Initial Notification Letter")
      {
        viewName = "InitialNotificationLetterAsPdfHistory";
      }
      return new ViewAsPdf(viewName, vm);
    }



    [Route("SendTitleReturnLetterAsPdf")]
    public async Task<IActionResult> SendTitleReturnLetterAsPdf(int id)
    {
      var vm = new InteractionViewModel();
      vm.Interaction = await _db.Interactions.FindAsync(id);
      const string viewName = "TitleReturnLetterAsPdf";
      vm.Interaction.InteractionType = "Title Return Letter";
      vm.County = await _db.Counties.FirstOrDefaultAsync();
      return new ViewAsPdf(viewName, vm);
    }
    [Route("ViewTitleReturnLetterAsPdf")]
    public async Task<IActionResult> ViewTitleReturnLetterAsPdf(int id)
    {
      var vm = new InteractionViewModel();
      vm.Interaction = await _db.Interactions.FindAsync(id);
      const string viewName = "TitleReturnLetterAsPdfHistory";
      vm.County = await _db.Counties.FirstOrDefaultAsync();
      return new ViewAsPdf(viewName, vm);
    }




    [Route("SendEmailToCustomer")]
    public ActionResult SendEmailToCustomer(int id)
    {
      var title = _db.Titles.Find(id);
      //TODO: Check email for null
      var msg = new MailMessage
      {
        From = new MailAddress(_db.Counties.Find(title.CountyId).OosTitleEmailAddress, _db.Counties.Find(title.CountyId).Name),
        Body = "<p>We have received the title work for the subject vehicle from the lien holder.  In order to complete the " +
          "registration process, you will need to bring in the following:</p>" +
          "<ul>" +
          "<li><b>EACH APPLICANT</b> who will be named on the title must be present with a valid driver license. </li>" +
          "<li>Proof of Florida insurance. </li>" +
          "<li>Bill of Sale (If the vehicle has been owned less than 6 months). </li>" +
          "<li>The vehicle must be present in order to verify the VIN (Vehicle Identification Number).</li> </ul>" +
          "<p>We require the title work to be completed within 30 days of the date that we received it " + title.RecDt.ToShortDateString() + ". </p>" +
          "<p>The title work is currently held at our Main Office. If you would like to have the title work delivered to one of our branch offices, which may be more convenient for you,  please call (904) 491‐7400 and choose option 2, Monday through Friday 8:00 am ‐ 5:00 pm. </p>" +
          "<p>For your convenience, we accept payment via cash, local checks, VISA, Master Card, American Express, or Discover Card (2.5% convenience fee, paid to the credit card processing company, will be applied for all " +
          "credit cards. The Tax Collector's Office does not keep any portion of this fee.)</p>" +
          "<p>Thank you for the opportunity to serve you! </p>" +
          "<p>Motor Services Department</p>" +
          "<p><b>" + _db.Counties.Find(title.CountyId).Name + "</b></p>",
        Subject = "TITLE RECEIVED for " + title.VehYr + " " + title.VehMake + " " + title.VehModel,
        IsBodyHtml = true,
        Bcc = { new MailAddress(_db.Counties.Find(title.CountyId).OosTitlesCopyEmailAddress) }
      };
      msg.To.Add(new MailAddress(title.CustEmail));
      var client = new SmtpClient
      {
        Host = _db.AppSettings.FirstOrDefaultAsync().Result.SmtpHost,
        Port = _db.AppSettings.FirstOrDefaultAsync().Result.SmtpPort
      };
      try
      { client.Send(msg); }
      catch (Exception e)
      { return RedirectToAction("Error", "Home", new { errmsg = "Email Failed : " + e.Message }); }

      var interaction = new Interaction()
      {
        TitleId = title.Id,
        InteractionDt = DateTime.Now,
        ReceivedDt = title.RecDt,
        InteractionUserId = _userManager.GetUserAsync(User).Result.Id.ToString(),
        InteractionType = "Email",
        VehYr = title.VehYr,
        VehMake = title.VehMake,
        VehModel = title.VehModel,
        Vin = title.Vin,
        TitleState = title.TitleState,
        IsNew = title.NewVeh,
        CustFName = title.CustFName,
        CustLName = title.CustLName,
        CustAddr1 = title.CustAddr1,
        CustAddr2 = title.CustAddr2,
        CustCity = title.CustCity,
        CustState = title.CustState,
        CustZip = title.CustZip,
        CustPhone = title.CustPhone ?? "N/A",
        CustEmail = title.CustEmail,
        CustPhone2 = title.CustPhone2,
        CustFName2 = title.CustFName2,
        CustLName2 = title.CustLName2,
        CustFName3 = title.CustFName3,
        CustLName3 = title.CustLName3,
        TitleRecievedFromType = (Interaction.TitleReceivedFromTypes)title.TitleRecievedFromType
      };
      _db.Interactions.Add(interaction);
      _db.SaveChanges();
      title.LastInteraction = interaction.Id;
      _db.Entry(title).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Details", "Titles", new { id });
    }


    protected override void Dispose(bool disposing)
    {
      if (disposing) _db.Dispose();
      base.Dispose(disposing);
    }
  }
}
