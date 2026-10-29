using FeedbackManagement.Models;

namespace FeedbackManagement.Services;

public interface IFeedbackRepository
{
    List<Feedback> GetAll();
    Feedback? Get(int id);
    void Add(Feedback f);
    void Update(Feedback f);
    void Delete(int id);
}

public class InMemoryFeedbackRepository : IFeedbackRepository
{
    private readonly List<Feedback> _items = new();
    private int _nextId = 1;
    private readonly object _lock = new();

    public InMemoryFeedbackRepository()
    {
        // Sample data so the list isn't empty on first run
        Add(new Feedback { Name = "Asha Patel", Email = "asha@example.com", Category = "Website",
                           Rating = 5, Comments = "Very clean and easy to use website." });
        Add(new Feedback { Name = "Ravi Shah", Email = "ravi@example.com", Category = "Support",
                           Rating = 4, Comments = "Support replied quickly and solved my issue." });
    }

    public List<Feedback> GetAll()
    {
        lock (_lock) return _items.OrderByDescending(x => x.SubmittedOn).ThenByDescending(x => x.Id).ToList();
    }

    public Feedback? Get(int id)
    {
        lock (_lock) return _items.FirstOrDefault(x => x.Id == id);
    }

    public void Add(Feedback f)
    {
        lock (_lock)
        {
            f.Id = _nextId++;
            f.SubmittedOn = DateTime.Now;
            _items.Add(f);
        }
    }

    public void Update(Feedback f)
    {
        lock (_lock)
        {
            var existing = _items.FirstOrDefault(x => x.Id == f.Id);
            if (existing == null) return;
            existing.Name = f.Name;
            existing.Email = f.Email;
            existing.Category = f.Category;
            existing.Rating = f.Rating;
            existing.Comments = f.Comments;
            existing.ContactBack = f.ContactBack;
        }
    }

    public void Delete(int id)
    {
        lock (_lock) _items.RemoveAll(x => x.Id == id);
    }
}
