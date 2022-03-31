namespace MandoWebApp.Models.ViewModels
{
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
