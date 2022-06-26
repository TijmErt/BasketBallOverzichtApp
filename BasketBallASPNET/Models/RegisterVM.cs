using System.ComponentModel.DataAnnotations;

namespace BasketBallASPNET.Models
{
    public class RegisterVM
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Geslacht{ get; set; }
        public DateTime GeboorteDatum{ get; set; }

        [Required(ErrorMessage = "Vul een email in")]
        public string Email{ get; set; }

        [Required(ErrorMessage = "Vul een wachtwoord in")]
        public string Wachtwoord{ get; set; }

        public string BevestigWachtwoord{ get; set; }
        public int ClubID { get; set; }
        public int RoleID { get; set; }
        public List<ClubVM> Clubs { get; set; }

        public RegisterVM()
        {

        }

        public RegisterVM(string firstName, string lastName, string geslacht, DateTime geboorteDatum, string email, int clubID, string wachtwoord, string bevestigWachtwoord)
        {
            FirstName = firstName;
            LastName = lastName;
            Geslacht = geslacht;
            GeboorteDatum = geboorteDatum;
            Email = email;
            Wachtwoord = wachtwoord;
            BevestigWachtwoord = bevestigWachtwoord;
            ClubID = clubID;
        }
    }
}
