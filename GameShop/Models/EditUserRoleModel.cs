using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace GameShop.Models.ViewModels
{
    public class EditUserRoleViewModel
    {
        public int UserId { get; set; }
        public string Username { get; set; }

        // Роль, яку ми вибрали у випадаючому списку
        public string SelectedRole { get; set; }

        // Список усіх можливих ролей для заповнення випадаючого списку
        public IEnumerable<SelectListItem> RolesList { get; set; }
    }
}
