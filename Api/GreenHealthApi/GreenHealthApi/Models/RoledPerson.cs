namespace GreenHealthApi.Models
{
    public class RoledPerson
    {
        public string Phone { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        public RoledPerson(string phone, string password, string role)
        {
            Phone = phone;
            Password = password;
            Role = role;
        }

        public RoledPerson()
        {
            
        }
    }
}