using Microsoft.AspNetCore.Mvc;
using Pre_Finals_Quiz.Data;
using Pre_Finals_Quiz.Models;

namespace Pre_Finals_Quiz.Controllers;

public class PortfolioController : Controller
{
    [HttpGet]
    public IActionResult Index(string? category)
    {
        var projects = string.IsNullOrWhiteSpace(category)
            ? PortfolioStore.Projects
            : PortfolioStore.Projects
                .Where(p => p.Category.Contains(category, StringComparison.OrdinalIgnoreCase)).ToList();
        ViewData["ActiveCategory"] = category;
        ViewData["Categories"] = PortfolioStore.Projects.Select(p => p.Category).Distinct().ToList();
        return View(projects);
    }

    [HttpGet]
    public IActionResult Details(int id)
    {
        var project = PortfolioStore.GetProject(id);
        if (project == null) return NotFound();
        ViewData["Comments"] = PortfolioStore.GetComments(id);
        ViewData["IsLoggedIn"] = HttpContext.Session.GetString("AuthUser") != null;
        return View(project);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult AddComment(int id, Comment input)
    {
        var project = PortfolioStore.GetProject(id);
        if (project == null) return NotFound();

        // Require login to comment (hardcoded login task requirement).
        if (HttpContext.Session.GetString("AuthUser") == null)
        {
            TempData["Error"] = "Please log in to post a comment.";
            return RedirectToAction("Login", "Account", new { returnUrl = Url.Action("Details", "Portfolio", new { id }) });
        }

        // Force server-side author to logged-in user if provided, else validate.
        var sessionName = HttpContext.Session.GetString("DisplayName");
        if (!string.IsNullOrWhiteSpace(sessionName))
            input.AuthorName = sessionName!;

        input.ProjectId = id;
        // Validate only AuthorName + Text.
        ModelState.Clear();
        TryValidateModel(input);

        if (!ModelState.IsValid)
        {
            ViewData["Comments"] = PortfolioStore.GetComments(id);
            ViewData["IsLoggedIn"] = true;
            TempData["Error"] = "Comment could not be posted. Check the highlighted fields.";
            return View("Details", project);
        }

        PortfolioStore.AddComment(id, input.AuthorName, input.Text);
        TempData["Success"] = "Comment posted!";
        return RedirectToAction(nameof(Details), new { id });
    }
}
