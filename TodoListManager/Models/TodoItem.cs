using System.Runtime.InteropServices.JavaScript;

namespace TodoListManager.Models;

using System;

public class TodoItem
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? CompletedDate { get; set; }
    public Priority Priority { get; set; }

    public TodoItem()
    {
        CreatedDate = DateTime.Now;
    }

    public override string ToString()
    {
        string status = IsCompleted ? "✓" : " ";
        string prioritySymbol = Priority switch
        {
            Priority.High => "!!",
            Priority.Medium => "!",
            Priority.Low => "",
            _ => ""
        };

        return $"[{status}] {Id}. {prioritySymbol} {Title} - {Description}";
    }
}