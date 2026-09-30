using Microsoft.AspNetCore.Mvc;
using Pre_Finals_Quiz.Models;
using System.Security.Cryptography;
using System.Text;

namespace Pre_Finals_Quiz.Controllers;

public class AccountController : Controller
{
    // Hardcoded demo credentials. Documented in README.md.
    // Username: admin | Password: Portfolio123!
    // Stored as SHA256 hash — plain password never stored in source beyond this demo constant.
    private const string DemoUsername = "admin";
    private const string DemoPassword = "Portfolio123!";

    private const string SessionUserKey = "AuthUser";
    private const string SessionDisplayKey = "DisplayName";

    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
    {
        if (HttpContext.Session.GetString(SessionUserKey) != null)
            return RedirectToAction("Index", "Portfolio");
        ViewData["ReturnUrl"] = returnUrl;
        return View(new LoginViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Login(LoginViewModel model, string? returnUrl = null)
    {
        if (!ModelState.IsValid)
            return View(model);

        var username = (model.Username ?? string.Empty).Trim();
        var password = model.Password ?? string.Empty;

        if (IsValidLogin(username, password))
        {
            HttpContext.Session.SetString(SessionUserKey, DemoUsername);
            HttpContext.Session.SetString(SessionDisplayKey, username);
            TempData["Success"] = $"Welcome back, {username}!";
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);
            return RedirectToAction("Index", "Portfolio");
        }

        // Generic message to avoid user enumeration.
        ModelState.AddModelError(string.Empty, "Invalid username or password.");
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        TempData["Success"] = "You have been logged out.";
        return RedirectToAction("Index", "Home");
    }

    public static bool IsValidLogin(string username, string password)
    {
        if (!string.Equals(username, DemoUsername, StringComparison.OrdinalIgnoreCase))
            return false;
        return SlowEquals(Sha256(DemoPassword), Sha256(password));
    }

    private static string Sha256(string input)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(input));
        return Convert.ToHexString(bytes);
    }

    // Constant-time comparison to mitigate timing attacks.
    private static bool SlowEquals(string a, string b)
    {
        if (a.Length != b.Length) return false;
        var diff = 0;
        for (var i = 0; i < a.Length; i++) diff |= a[i] ^ b[i];
        return diff == 0;
    }
}
