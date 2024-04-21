using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;

namespace MyProject.Pages
{
    public partial class Login
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        public string ErrorMessage { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        private void HandleLoginSubmit()
        {
            if (Username == "user" && Password == "password")
            {
                NavigationManager.NavigateTo("/Index");
            }
            else
            {
                ErrorMessage = "Invalid username or password.";
            }
        }
    }
}
