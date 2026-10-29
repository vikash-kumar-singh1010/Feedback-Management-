using FeedbackManagement.Models;
using FeedbackManagement.Services;
using Microsoft.AspNetCore.Mvc;

namespace FeedbackManagement.Controllers;

// FLOW 1: Classic multi-page flow (Index -> Create -> redirect back to Index)
public class FeedbackController : Controller
{
    private readonly IFeedbackRepository _repo;
    public FeedbackController(IFeedbackRepository repo) => _repo = repo;

    public IActionResult Index() => View(_repo.GetAll());

    public IActionResult Create() => View(new Feedback());

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Create(Feedback model)
    {
        if (!ModelState.IsValid) return View(model);   // server-side validation

        _repo.Add(model);
        TempData["Success"] = "Thank you! Your feedback was submitted.";
        return RedirectToAction(nameof(Index));        // Post-Redirect-Get
    }

    public IActionResult Edit(int id)
    {
        var fb = _repo.Get(id);
        return fb == null ? NotFound() : View(fb);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Edit(Feedback model)
    {
        if (!ModelState.IsValid) return View(model);
        _repo.Update(model);
        TempData["Success"] = "Feedback updated.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        _repo.Delete(id);
        TempData["Success"] = "Feedback deleted.";
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Error() => Content("Something went wrong.");
}
