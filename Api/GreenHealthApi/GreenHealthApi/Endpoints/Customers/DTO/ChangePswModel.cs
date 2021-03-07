namespace GreenHealthApi.Endpoints.Customers.DTO
{
    public class ChangePswModel
    {
        public string OldPass { get; set; }
        public string NewPass { get; set; }

        public ChangePswModel(string oldPass, string newPass)
        {
            OldPass = oldPass;
            NewPass = newPass;
        }

        public ChangePswModel()
        {
            
        }
    }
}