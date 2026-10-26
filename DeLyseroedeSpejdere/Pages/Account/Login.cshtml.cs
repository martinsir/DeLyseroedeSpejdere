using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using DeLyseroedeSpejdere.Models;
using DeLyseroedeSpejdere.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DeLyseroedeSpejdere.Pages.Account;

[AllowAnonymous]
public class LoginModel(IUserRepository users, IPasswordHasher<User> hasher) : PageModel
{
    [BindProperty] public LoginInput Input { get; set; } = new();
    public string? ReturnUrl { get; set; }

    public void OnGet(string? returnUrl = null) { ReturnUrl = returnUrl; }

    public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
    {
        ReturnUrl = returnUrl;
        if (!ModelState.IsValid) return Page();
        var user = await users.GetByUsernameAsync(Input.Username.Trim());
        if (user == null || hasher.VerifyHashedPassword(user, user.PasswordHash, Input.Password)
            == PasswordVerificationResult.Failed)
        {
            ModelState.AddModelError("", "Forkert brugernavn eller adgangskode.");
            return Page();
        }

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.FullName),
            new(ClaimTypes.Role, user.Role)
        };
        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(identity), new AuthenticationProperties { IsPersistent = false });
        if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl)) return LocalRedirect(returnUrl);
        return RedirectToPage(user.Role == "Leader" ? "/Leader/Index" : "/Activities/Index");
    }

    public class LoginInput
    {
        [Required(ErrorMessage = "Skriv brugernavn.")]
        [Display(Name = "Brugernavn")]
        public string Username { get; set; } = "";
        [Required(ErrorMessage = "Skriv adgangskode.")]
        [DataType(DataType.Password)]
        [Display(Name = "Adgangskode")]
        public string Password { get; set; } = "";
    }
}
