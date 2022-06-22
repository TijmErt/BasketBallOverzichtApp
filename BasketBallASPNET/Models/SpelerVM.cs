using BusnLogic.Entity;

namespace BasketBallASPNET.Models
{
    public class SpelerVM
    {
        public int? ID;
        public string FirstName;
        public string LastName;
        public string Geslacht;
        public DateTime GeboorteDatum;
        public string Email;
        public int? TeamID;
        public int? ClubID;
        public int? SpelerNummer;

        public SpelerVM(int? id, string firstName, string lastName, DateTime geboorteDatum, string geslacht, int? teamID, int? clubID, string email, int? SpelerNummer)
        {
            this.ID = id;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.GeboorteDatum = geboorteDatum;
            this.Geslacht = geslacht;
            this.Email = email;
            this.TeamID = teamID;
            this.ClubID = clubID;
            this.SpelerNummer = SpelerNummer;
        }
        public SpelerVM(Gebruiker Temp)
        {
            this.ID = Temp.ID;
            this.FirstName = Temp.FirstName;
            this.LastName = Temp.LastName;
            this.Geslacht = Temp.Geslacht;
            this.GeboorteDatum = Temp.GeboorteDatum;
            this.Email = Temp.Email;
            this.TeamID = Temp.TeamID;
            this.ClubID = Temp.ClubID;
            this.SpelerNummer = Temp.SpelerNummer;
        }
        public SpelerVM()
        {

        }
    }
}
