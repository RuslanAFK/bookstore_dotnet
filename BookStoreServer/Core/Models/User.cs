namespace BookStoreServer.Core.Models
{
    public class User : ISearchable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
