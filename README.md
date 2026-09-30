# My Portfolio — ASP.NET Core MVC (Pre-Finals Quiz)

Modern portfolio MVC application presenting projects with thumbnails, GitHub links,
a table of contents, per-project detail pages, and per-project comment sections.

## Login Details (Hardcoded Demo Account)

| Field | Value |
|---|---|
| Login URL | `/Account/Login` |
| Username | `admin` |
| Password | `Portfolio123!` |

> The account is hardcoded in `Controllers/AccountController.cs` (`DemoUsername` /
> `DemoPassword`). Passwords are compared via SHA-256 + constant-time compare.
> Auth state is stored in server-side Session (`AuthUser` / `DisplayName`).
> Comments require login — anonymous POSTs are redirected to `/Account/Login?returnUrl=...`.

## Features (Rubric Coverage)

- **UI design:** custom modern theme (`wwwroot/css/site.css`), hero, cards, TOC, responsive grid.
- **Completeness:**
  - `/` (Home) — hero, stats, Table of Contents, project cards with thumbnails + GitHub links.
  - `/Portfolio` — full Table of Contents + filterable project gallery.
  - `/Portfolio/Details/{id}` — detail page per project (long description, tech stack,
    features, GitHub + demo links, thumbnail) + comment section per project.
  - `/Account/Login` + Logout — hardcoded login.
- **Security:** Session auth, `[ValidateAntiForgeryToken]` on Login/Logout/AddComment,
  server-side DataAnnotations validation, Razor auto-encoding (XSS-safe comments),
  generic login error (no user enumeration), `Url.IsLocalUrl` check on returnUrl,
  SHA-256 comparison, HttpOnly session cookie.
- **Code velocity:** in-memory `Data/PortfolioStore.cs` (no DB setup needed), 3 seeded
  projects, 3 seeded comments.

## Projects

| # | Project | GitHub |
|---|---|---|
| 1 | Prelim Assignment 1 — ConsoleApp1 Solution | https://github.com/kykie-hub/IT_ELECTIVE_2_OCAMPO_KYLIE_BSIT31E1 |
| 2 | Pre-Finals Activity 1 — WebApplication1 | https://github.com/kykie-hub/IT_ELECTIVE_2_A1_PREFINALS_OCAMPO_KYLIE |
| 3 | Pre-Final Exam — ExamApp (MVC) | https://github.com/kykie-hub/IT_ELECTIVE_2_31E1_PREFINAL_EXAM_Ocampo_Kylie |

Thumbnails: `wwwroot/images/*.svg` (local, no external dependency).

## Run

```powershell
dotnet build
dotnet run
# open https://localhost:5001 (or the URL shown in the console)
```

## Structure

```
Controllers/
  HomeController.cs        # Landing page (hero + TOC preview)
  PortfolioController.cs   # Index (TOC + gallery), Details + AddComment
  AccountController.cs     # Hardcoded Login / Logout (session)
Models/
  Project.cs  Comment.cs  LoginViewModel.cs
Data/
  PortfolioStore.cs        # In-memory projects + thread-safe comments
Views/
  Home/Index.cshtml
  Portfolio/Index.cshtml   # TOC + cards
  Portfolio/Details.cshtml # Detail + comments + mini-TOC
  Account/Login.cshtml
  Shared/_Layout.cshtml
wwwroot/
  css/site.css  images/*.svg
```
