using TodoListManager.Models;

namespace TodoListManager.Services;

public interface IStorageService
{
    void SaveTodos(List<TodoItem> todos);
    List<TodoItem> Load();
}