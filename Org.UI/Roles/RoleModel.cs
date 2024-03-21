using System.ComponentModel.DataAnnotations;

namespace Org.UI.Roles;

internal class RoleModel
{
    [Required(ErrorMessage = "Code obligatoire")]
    public string RoleCode { get; set; }

    [Required(ErrorMessage = "Nom obligatoire")]
    public string RoleName { get; set; }
}