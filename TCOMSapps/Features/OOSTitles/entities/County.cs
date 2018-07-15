using System.Collections.Generic;


namespace TCOMSapps.Features.OOSTitles.entities
{
  public class County
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string PocUserId { get; set; }
    public bool SharedData { get; set; }
    public bool Active { get; set; }
    public int DefaultLocation { get; set; }
    public int Theme { get; set; }

    public List<Location> Locations { get; set; }

    public string OosTitleEmailAddress { get; set; }

    public string OosTitlesCopyEmailAddress { get; set; }

    public string OosTitleLetterAddressLine1 { get; set; }

    public string OosTitleLetterAddressLine2 { get; set; }

    public string OosTitleLetterCity { get; set; }

    public string OosTitleLetterState { get; set; }

    public string OosTitleLetterZip { get; set; }

    public string OosTitleLetterPhoneNumbers { get; set; }

    public string OosTitleLetterTaxCollectorName { get; set; }

    public string OosTitleLetterWebsite { get; set; }

  }
}