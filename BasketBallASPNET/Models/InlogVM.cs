namespace BasketBallASPNET.Models
{
    public class InlogVM
    {
        public string Email { get; set; }
        public string Wachtwoord { get; set; }

        public InlogVM()
        {

        }

        public InlogVM(string email, string wachtwoord)
        {
            this.Email = email;
            this.Wachtwoord = wachtwoord;
        }
    }
}
