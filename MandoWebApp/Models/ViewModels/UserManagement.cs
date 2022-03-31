namespace MandoWebApp.Models.ViewModels
{
    public class UserManagement
    {
        public List<string> AllRoles { get; set; }
        public List<UserManagementItem> Users { get; set; }

        public UserManagement(IEnumerable<string> roles, IEnumerable<UserManagementItem> users)
        {
            AllRoles = roles.ToList();
            Users = users.ToList();
        }
    }

    public class UserManagementItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<string> Roles { get; set; }

        public UserManagementItem(string id, string name, IEnumerable<string> roles)
        {
            Id = id;
            Name = name;
            Roles = roles.ToList();
        }
    }
}
