using System.ComponentModel.DataAnnotations;

namespace Pre_Finals_Quiz.Models;

public class Comment
{
    public int Id { get; set; }
    public int ProjectId { get; set; }

    [Required(ErrorMessage = "Name is required.")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be 2-50 characters.")]
    public string AuthorName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Comment text is required.")]
    [StringLength(500, MinimumLength = 2, ErrorMessage = "Comment must be 2-500 characters.")]
    public string Text { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
