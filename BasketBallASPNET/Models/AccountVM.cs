using System.ComponentModel.DataAnnotations;

namespace BasketBallASPNET.Models
{
    public class AccountVM
    {
        public int ID;
        public string FirstName;
        public string LastName;
        public string Geslacht;
        public DateTime GeboorteDatum;

        [Required(ErrorMessage = "Vul een email in")]
        public string Email;

        [Required(ErrorMessage = "Vul een wachtwoord in")]
        public string Wachtwoord;

        public string BevestigWachtwoord;

        public AccountVM(int iD, string firstName, string lastName, string geslacht, DateTime geboorteDatum, string email, string wachtwoord, string bevestigWachtwoord)
        {
            ID = iD;
            FirstName = firstName;
            LastName = lastName;
            Geslacht = geslacht;
            GeboorteDatum = geboorteDatum;
            Email = email;
            Wachtwoord = wachtwoord;
            BevestigWachtwoord = bevestigWachtwoord;
        }
    }
}
