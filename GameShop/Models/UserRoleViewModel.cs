using System.Collections.Generic;

namespace GameShop.Models.ViewModels
{
    public class UserRolesViewModel
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
