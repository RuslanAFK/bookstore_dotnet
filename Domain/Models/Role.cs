using System.Collections.ObjectModel;

namespace BookStoreServer.Core.Models;

public class Role
{
    public int RoleId { get; set; }
    public string RoleName { get; set; }
    public ICollection<User> Users { get; set; }

    public Role()
    {
        Users = new Collection<User>();
    }
}