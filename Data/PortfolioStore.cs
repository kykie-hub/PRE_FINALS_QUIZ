using Pre_Finals_Quiz.Models;

namespace Pre_Finals_Quiz.Data;

public static class PortfolioStore
{
    public static readonly List<Project> Projects = new()
    {
        new Project
        {
            Id = 1,
            Title = "Prelim Assignment 1 — ConsoleApp1 Solution",
            Category = "Prelim / C# Foundations",
            ShortDescription = "Prelim assignment built from the class template: a C# solution with a console app and a web app project.",
            LongDescription = "IT Elective 2 prelim assignment (BSIT 31E1), forked from the official class template. The solution contains ConsoleApp1 — a C# console application covering programming fundamentals — plus WebApplication2, an introductory web project. It demonstrates solution/project organisation in Visual Studio, .NET SDK-style projects, and version control with Git and GitHub.",
            GitHubUrl = "https://github.com/kykie-hub/IT_ELECTIVE_2_OCAMPO_KYLIE_BSIT31E1",
            ThumbnailUrl = "/images/prelim-a1.svg",
            Technologies = new[] { "C#", ".NET", "Console App", "ASP.NET", "Git & GitHub" },
            Features = new[] { "ConsoleApp1 fundamentals project", "WebApplication2 starter web project", "Solution (.sln) organisation in Visual Studio", "Forked class-template workflow", "Version-controlled with Git & GitHub" }
        },
        new Project
        {
            Id = 2,
            Title = "Pre-Finals Activity 1 — WebApplication1",
            Category = "Pre-Finals / Web App",
            ShortDescription = "Pre-finals activity 1: a .NET web application project (WebApplication1) built with the modern SDK-style solution format.",
            LongDescription = "IT Elective 2 pre-finals Activity 1. A .NET web application built as WebApplication1 using the modern .slnx solution format. This activity practices creating, building and running a web project from scratch in Visual Studio — the foundation skills reused in later MVC work such as the pre-final exam app.",
            GitHubUrl = "https://github.com/kykie-hub/IT_ELECTIVE_2_A1_PREFINALS_OCAMPO_KYLIE",
            ThumbnailUrl = "/images/prefinals-a1.svg",
            Technologies = new[] { "C#", ".NET", "ASP.NET Core", "Visual Studio", "Git & GitHub" },
            Features = new[] { "Web project created from scratch", "Modern .slnx solution format", "Build & run with launch profiles", "Static files via wwwroot", "Version-controlled with Git & GitHub" }
        },
        new Project
        {
            Id = 3,
            Title = "Pre-Final Exam — ExamApp (MVC)",
            Category = "Pre-Final Exam / MVC",
            ShortDescription = "Pre-final exam project: a full ASP.NET Core MVC app (ExamApp) with controllers, models, views and static files.",
            LongDescription = "IT Elective 2 pre-final exam submission (Ocampo, Kylie — BSIT 31E1). ExamApp is a complete ASP.NET Core MVC application with Controllers, Models, Views, wwwroot static files, Program.cs startup and appsettings configuration, developed over 23 commits. It demonstrates the full MVC request pipeline — routing, model binding, Razor views and static-file serving.",
            GitHubUrl = "https://github.com/kykie-hub/IT_ELECTIVE_2_31E1_PREFINAL_EXAM_Ocampo_Kylie",
            ThumbnailUrl = "/images/prefinal-exam.svg",
            Technologies = new[] { "ASP.NET Core MVC", "C#", "Razor Views", "Bootstrap", "Git & GitHub" },
            Features = new[] { "Full MVC structure (Controllers/Models/Views)", "Razor views with layout", "Static files via wwwroot", "appsettings configuration", "23-commit development history" }
        }
    };

    private static readonly List<Comment> _comments = new()
    {
        new Comment { Id = 1, ProjectId = 1, AuthorName = "Maya", Text = "Nice and clean solution structure for a first assignment!", CreatedAt = DateTime.UtcNow.AddDays(-4) },
        new Comment { Id = 2, ProjectId = 3, AuthorName = "Leo", Text = "Full MVC pipeline with views and static files — great exam work.", CreatedAt = DateTime.UtcNow.AddDays(-2) },
        new Comment { Id = 3, ProjectId = 3, AuthorName = "Admin", Text = "Welcome! Ask me anything about how ExamApp was built.", CreatedAt = DateTime.UtcNow.AddDays(-1) }
    };
    private static int _nextCommentId = 4;
    private static readonly object _lock = new();

    public static Project? GetProject(int id) => Projects.FirstOrDefault(p => p.Id == id);

    public static List<Comment> GetComments(int projectId)
    {
        lock (_lock)
        {
            return _comments.Where(c => c.ProjectId == projectId)
                .OrderByDescending(c => c.CreatedAt).ToList();
        }
    }

    public static Comment AddComment(int projectId, string authorName, string text)
    {
        lock (_lock)
        {
            var comment = new Comment
            {
                Id = _nextCommentId++,
                ProjectId = projectId,
                AuthorName = authorName.Trim(),
                Text = text.Trim(),
                CreatedAt = DateTime.UtcNow
            };
            _comments.Add(comment);
            return comment;
        }
    }

    public static int CommentCount(int projectId)
    {
        lock (_lock) { return _comments.Count(c => c.ProjectId == projectId); }
    }
}
