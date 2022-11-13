namespace FarmFresh.Framework.Models.Requests
{
    public class AddUserRequest
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Phone { get; set; }

        public Guid? CreatedBy { get; set; }

        public IList<long> Roles { get; set; }
    }
}
