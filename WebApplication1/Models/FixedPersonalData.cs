using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class FixedPersonalData
    {
        public string PersonalNumber { get; set; }
        public string Surname { get; set; }
        public string GivenName { get; set; }
        public string ParentGivenName { get; set; }
        public string Sex { get; set; }
        public string PlaceOfBirth { get; set; }
        public string StateOfBirth { get; set; }
        public string DateOfBirth { get; set; }
        public string CommunityOfBirth { get; set; }
        public string StatusOfForeigner { get; set; }
        public string NationalityFull { get; set; }
    }
}
