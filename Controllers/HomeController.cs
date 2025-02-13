using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using NotesApp.Models;

public class HomeController : Controller
{
    private readonly AppDbContext _context;
    private readonly EncryptionService _encryption;

    public HomeController(AppDbContext context, EncryptionService encryption)
    {
        _context = context;
        _encryption = encryption;
    }

    public IActionResult Index()
    {
        var categories = _context.Categories.Include(c => c.Notes).ToList();
        return View(categories);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        var errorViewModel = new ErrorViewModel
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
            ErrorMessage = TempData["ErrorMessage"] as string
        };
        return View(errorViewModel);
    }

    [HttpPost]
    public IActionResult CreateCategory(string name)
    {
        try
        {
            if (!string.IsNullOrEmpty(name))
            {
                _context.Categories.Add(new Category { Name = name });
                _context.SaveChanges();
            }
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = ex.Message;
            return RedirectToAction("Error");
        }

        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult AddNote(Guid categoryId, string content)
    {
        try
        {
            if (!string.IsNullOrEmpty(content))
            {
                var category = _context.Categories.Find(categoryId);
                if (category == null)
                {
                    TempData["ErrorMessage"] = $"Category with ID {categoryId} not found.";
                    return RedirectToAction("Error");
                }

                var encryptedContent = _encryption.Encrypt(content);
                _context.Notes.Add(new Note
                {
                    CategoryId = categoryId,
                    Content = encryptedContent
                });
                _context.SaveChanges();
            }
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = ex.Message;
            return RedirectToAction("Error");
        }

        return RedirectToAction("Index");
    }

    public IActionResult ViewNotes(Guid categoryId)
    {
        var category = _context.Categories.Include(c => c.Notes).FirstOrDefault(c => c.Id == categoryId);
        if (category == null)
        {
            TempData["ErrorMessage"] = $"Category with ID {categoryId} not found.";
            return RedirectToAction("Error");
        }

        // Расшифровка содержимого заметок
        foreach (var note in category.Notes)
        {
            note.Content = _encryption.Decrypt(note.Content);
        }

        return View(category);
    }

    [HttpPost]
    public IActionResult DeleteNote(Guid noteId)
    {
        var note = _context.Notes.Find(noteId);
        if (note == null)
        {
            TempData["ErrorMessage"] = $"Note with ID {noteId} not found.";
            return RedirectToAction("Error");
        }

        _context.Notes.Remove(note);
        _context.SaveChanges();

        return RedirectToAction("ViewNotes", new { categoryId = note.CategoryId });
    }

    [HttpPost]
    public IActionResult DeleteCategory(Guid categoryId)
    {
        var category = _context.Categories.Include(c => c.Notes).FirstOrDefault(c => c.Id == categoryId);
        if (category == null)
        {
            TempData["ErrorMessage"] = $"Category with ID {categoryId} not found.";
            return RedirectToAction("Error");
        }

        _context.Notes.RemoveRange(category.Notes);
        _context.Categories.Remove(category);
        _context.SaveChanges();

        return RedirectToAction("Index");
    }
}