namespace Pre_Finals_Quiz.Models;

public class Project
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string ShortDescription { get; set; } = string.Empty;
    public string LongDescription { get; set; } = string.Empty;
    public string GitHubUrl { get; set; } = string.Empty;
    public string? LiveDemoUrl { get; set; }
    public string ThumbnailUrl { get; set; } = string.Empty;
    public string[] Technologies { get; set; } = Array.Empty<string>();
    public string[] Features { get; set; } = Array.Empty<string>();
}
