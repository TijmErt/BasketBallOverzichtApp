using BusnLogic.Entity;

namespace BasketBallASPNET.Models
{
    public class SpelerVM
    {
        public long ID;
        public string FirstName;
        public string LastName;
        public string Geslacht;
        public DateTime GeboorteDatum;
        public string Email;

        public SpelerVM(long id, string firstName, string lastName, DateTime geboorteDatum, string geslacht, string wachtwoord, string email)
        {
            this.ID = id;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.GeboorteDatum = geboorteDatum;
            this.Geslacht = geslacht;
            this.Email = email;
        }
        public SpelerVM(Gebruiker Temp)
        {
            this.ID = Temp.ID;
            this.FirstName = Temp.FirstName;
            this.LastName = Temp.LastName;
            this.Geslacht = Temp.Geslacht;
            this.GeboorteDatum = Temp.GeboorteDatum;
            this.Email = Temp.Email;
        }
        public SpelerVM()
        {

        }
    }
}
