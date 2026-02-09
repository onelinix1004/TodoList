using TodoListManager.Managers;
using TodoListManager.Services;
using TodoListManager.UI;

namespace TodoListManager;

class Program
{
    static void Main(string[] args)
    {
        // Dependency injection
        IStorageService storageService = new JsonStorageService("todos.json");
        TodoManager manager = new TodoManager(storageService);
        MenuUI menuUI = new MenuUI(manager);
        
        menuUI.Run();
    }
}