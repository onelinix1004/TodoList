using TodoListManager.Models;
using TodoListManager.Services;

namespace TodoListManager.Managers;

public class TodoManager
{
    private List<TodoItem> todos;
    private readonly IStorageService storageService;
    private int nextId = 1;
    
    public TodoManager(IStorageService storageService)
    {
        this.storageService = storageService;
        todos = new List<TodoItem>();
        LoadTodos();
    }
    
    // Add new todo
    public void AddTodo(string title, string description, Priority priority)
    {
        var todo = new TodoItem
        {
            Id = nextId++,
            Title = title,
            Description = description,
            Priority = priority,
            IsCompleted = false
        };
        todos.Add(todo);
        SaveTodos();
        Console.WriteLine($"\n✓ Todo đã được thêm thành công với ID: {todo.Id}");
    }
    
    // Display all todos
    public void DisplayTodos(bool showCompleted = true)
    {
        var filteredTodos = showCompleted 
            ? todos 
            : todos.Where(t => !t.IsCompleted).ToList();

        if (filteredTodos.Count == 0)
        {
            Console.WriteLine("\nKhông có todo nào!");
            return;
        }

        Console.WriteLine("\n" + new string('=', 80));
        Console.WriteLine("DANH SÁCH TODO");
        Console.WriteLine(new string('=', 80));

        var orderedTodos = filteredTodos.OrderByDescending(t => t.Priority)
            .ThenBy(t => t.IsCompleted)
            .ThenBy(t => t.CreatedDate);

        foreach (var todo in orderedTodos)
        {
            Console.ForegroundColor = todo.IsCompleted 
                ? ConsoleColor.DarkGray 
                : ConsoleColor.White;
                    
            if (todo.Priority == Priority.High && !todo.IsCompleted)
                Console.ForegroundColor = ConsoleColor.Red;
            else if (todo.Priority == Priority.Medium && !todo.IsCompleted)
                Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine(todo);
            Console.ResetColor();
        }
        Console.WriteLine(new string('=', 80));
    }
    
    // Mark todo as completed
    public void CompleteTodo(int id)
    {
        var todo = todos.FirstOrDefault(t => t.Id == id);
        if (todo == null)
        {
            Console.WriteLine($"\nKhông tìm thấy todo với ID: {id}");
            return;
        }

        todo.IsCompleted = true;
        todo.CompletedDate = DateTime.Now;
        SaveTodos();
        Console.WriteLine($"\n✓ Todo '{todo.Title}' đã được đánh dấu hoàn thành!");
    }

    // Delete todo
    public void DeleteTodo(int id)
    {
        var todo = todos.FirstOrDefault(t => t.Id == id);
        if (todo == null)
        {
            Console.WriteLine($"\nKhông tìm thấy todo với ID: {id}");
            return;
        }

        todos.Remove(todo);
        SaveTodos();
        Console.WriteLine($"\n✓ Todo '{todo.Title}' đã được xóa!");
    }
    
    // Edit todo
    public void EditTodo(int id, string newTitle, string newDescription, 
        Priority newPriority)
    {
        var todo = todos.FirstOrDefault(t => t.Id == id);
        if (todo == null)
        {
            Console.WriteLine($"\nKhông tìm thấy todo với ID: {id}");
            return;
        }

        if (!string.IsNullOrWhiteSpace(newTitle))
            todo.Title = newTitle;
        if (!string.IsNullOrWhiteSpace(newDescription))
            todo.Description = newDescription;
        todo.Priority = newPriority;

        SaveTodos();
        Console.WriteLine($"\n✓ Todo đã được cập nhật!");
    }

    // Search todos
    public void SearchTodos(string keyword)
    {
        var results = todos.Where(t => 
            t.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
            t.Description.Contains(keyword, StringComparison.OrdinalIgnoreCase)
        ).ToList();

        if (results.Count == 0)
        {
            Console.WriteLine($"\nKhông tìm thấy todo nào với từ khóa: '{keyword}'");
            return;
        }

        Console.WriteLine($"\nKết quả tìm kiếm cho '{keyword}':");
        foreach (var todo in results)
        {
            Console.WriteLine(todo);
        }
    }
    
    // Show statistics
    public void ShowStatistics()
    {
        int total = todos.Count;
        int completed = todos.Count(t => t.IsCompleted);
        int pending = total - completed;
        int high = todos.Count(t => t.Priority == Priority.High && !t.IsCompleted);

        Console.WriteLine("\n" + new string('=', 50));
        Console.WriteLine("THỐNG KÊ TODO");
        Console.WriteLine(new string('=', 50));
        Console.WriteLine($"Tổng số todo: {total}");
        Console.WriteLine($"Đã hoàn thành: {completed}");
        Console.WriteLine($"Chưa hoàn thành: {pending}");
        Console.WriteLine($"Ưu tiên cao (chưa xong): {high}");
        if (total > 0)
            Console.WriteLine($"Tỷ lệ hoàn thành: {(completed * 100.0 / total):F1}%");
        Console.WriteLine(new string('=', 50));
    }

    // Save to file
    private void SaveTodos()
    {
        storageService.SaveTodos(todos);
    }

    // Load from file
    private void LoadTodos()
    {
        todos = storageService.Load();
            
        if (todos.Count > 0)
            nextId = todos.Max(t => t.Id) + 1;
    }
}
