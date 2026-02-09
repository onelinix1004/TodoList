using System.Text.Json;
using TodoListManager.Models;

namespace TodoListManager.Services;

public class JsonStorageService : IStorageService
{

    private readonly string filePath;
    
    public JsonStorageService(string filePath = "todos.json")
    {
        this.filePath = filePath;
    }
    
    public void SaveTodos(List<TodoItem> todos)
    {
        try
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            
            string json = JsonSerializer.Serialize(todos, options);
            File.WriteAllText(filePath, json);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error saving todos: {e.Message}");
        }
    }
    
    public List<TodoItem> Load()
    {
        try
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                return JsonSerializer.Deserialize<List<TodoItem>>(json) ?? new List<TodoItem>();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error loading todos: {e.Message}");
        }
        
        return new List<TodoItem>();
    }
}