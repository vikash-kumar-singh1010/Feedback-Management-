using FeedbackManagement.Models;
using FeedbackManagement.Services;
using Microsoft.AspNetCore.Mvc;

namespace FeedbackManagement.Controllers;

// FLOW 2: Single page. Form and list are partial views refreshed via AJAX.
public class QuickController : Controller
{
    private readonly IFeedbackRepository _repo;
    public QuickController(IFeedbackRepository repo) => _repo = repo;

    // Full page (layout + form + list)
    public IActionResult Index() => View(new Feedback());

    // Partial: list only
    public IActionResult List() => PartialView("_List", _repo.GetAll());

    // Partial: returns form (with errors) or a fresh form (after success)
    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Create(Feedback model)
    {
        if (!ModelState.IsValid)
            return PartialView("_Form", model);

        _repo.Add(model);
        Response.Headers["X-Saved"] = "true";
        ModelState.Clear();
        return PartialView("_Form", new Feedback());
    }

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        _repo.Delete(id);
        return PartialView("_List", _repo.GetAll());
    }
}
