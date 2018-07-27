using System.Collections.Generic;
using System.ComponentModel;


namespace TCOMSapps.Features.OOSTitles.entities
{
  public class County
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string PocUserId { get; set; }
    public bool SharedData { get; set; }
    public bool Active { get; set; }

    [DisplayName("Default Location")]
    public int DefaultLocation { get; set; }
    public int Theme { get; set; }

    public List<Location> Locations { get; set; }

    [DisplayName("Email Address")]
    public string OosTitleEmailAddress { get; set; }

    [DisplayName("Cc Email Address")]
    public string OosTitlesCopyEmailAddress { get; set; }

    [DisplayName("Address Line 1")]
    public string OosTitleLetterAddressLine1 { get; set; }

    [DisplayName("Address Line 2")]
    public string OosTitleLetterAddressLine2 { get; set; }

    [DisplayName("City")]
    public string OosTitleLetterCity { get; set; }

    [DisplayName("State")]
    public string OosTitleLetterState { get; set; }

    [DisplayName("Zip Code")]
    public string OosTitleLetterZip { get; set; }

    [DisplayName("Phone Number(s)")]
    public string OosTitleLetterPhoneNumbers { get; set; }

    [DisplayName("Motor Services Phone Number")]
    public string OosTitleLetterMsPhoneNumber { get; set; }

    [DisplayName("Tax Collector Name")]
    public string OosTitleLetterTaxCollectorName { get; set; }

    [DisplayName("Website")]
    public string OosTitleLetterWebsite { get; set; }

  }
}