using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FrannHammer.Models
{
    public class CreateRoleBindingModel
    {
        [Required]
        [StringLength(256, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        [Display(Name = "Role Name")]
        public string Name { get; set; }
    }

    public class UsersInRoleModel
    {
        public string Id { get; set; }
        public List<string> EnrolledUsers { get; set; }
        public List<string> RemovedUsers { get; set; }
    }

    public class UserToRoleModel
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }
    }
}