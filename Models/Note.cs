using System.ComponentModel.DataAnnotations;

public class Note
{
    public Guid Id { get; set; }

    public Guid CategoryId { get; set; }

    [Required(ErrorMessage = "Содержимое заметки не может быть пустым")]
    public string Content { get; set; } = string.Empty;

    public Category? Category { get; set; }
}