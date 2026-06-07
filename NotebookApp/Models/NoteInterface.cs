namespace NotebookApp.Models;


public interface INote
{
    public NoteType Type { get; set; }
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public List<OneTask> Tasks { get; set; }
    public string Tag { get; set; }
    public string Color { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsPinned { get; set; }
    public DateTime EditedAt { get; set; }
    public List<DateTime> Dates { get; set; }
    public bool Star { get; set; }
    public bool IsAddedToCalendar { get; set; }
}

public enum NoteType
{
    Text,
    Tasks
}