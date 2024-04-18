using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class LoginModel : PageModel
{
    [BindProperty]
    public string Username { get; set; }

    [BindProperty]
    public string Password { get; set; }

    public string ErrorMessage { get; set; }

    public void OnGet()
    {
       
    }

    public IActionResult OnPost()
    {
        
        if (Username == "user" && Password == "password")
        {
           
            return RedirectToPage("/Index");
        }
        else
        {
           
            ErrorMessage = "Invalid username or password.";
            return Page();
        }
    }
}
