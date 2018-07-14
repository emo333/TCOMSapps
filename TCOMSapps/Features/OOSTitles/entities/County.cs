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


    }
}