using TodoListManager.Managers;
using TodoListManager.Models;

namespace TodoListManager.UI;

public class MenuUI
{
    private readonly TodoManager manager;

        public MenuUI(TodoManager manager)
        {
            this.manager = manager;
        }

        public void Run()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            bool running = true;

            ShowWelcome();

            while (running)
            {
                ShowMenu();
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddNewTodo();
                        break;
                    case "2":
                        manager.DisplayTodos();
                        break;
                    case "3":
                        manager.DisplayTodos(false);
                        break;
                    case "4":
                        MarkTodoComplete();
                        break;
                    case "5":
                        EditExistingTodo();
                        break;
                    case "6":
                        DeleteExistingTodo();
                        break;
                    case "7":
                        SearchForTodos();
                        break;
                    case "8":
                        manager.ShowStatistics();
                        break;
                    case "9":
                        running = false;
                        ShowGoodbye();
                        break;
                    default:
                        Console.WriteLine("\nLựa chọn không hợp lệ! Vui lòng thử lại.");
                        break;
                }

                if (running)
                {
                    Console.WriteLine("\nNhấn phím bất kỳ để tiếp tục...");
                    Console.ReadKey();
                }
            }
        }

        private void ShowWelcome()
        {
            Console.WriteLine(" TODO LIST MANAGER - QUẢN LÝ CÔNG VIỆC ");
        }

        private void ShowGoodbye()
        {
            Console.WriteLine("\nCảm ơn bạn đã sử dụng Todo List Manager!");
        }

        private void ShowMenu()
        {
            Console.WriteLine(" MENU CHÍNH");
            Console.WriteLine(" 1. Thêm todo mới");
            Console.WriteLine(" 2. Xem tất cả todo");
            Console.WriteLine(" 3. Xem todo chưa hoàn thành");
            Console.WriteLine(" 4. Đánh dấu hoàn thành");
            Console.WriteLine(" 5. Sửa todo");
            Console.WriteLine(" 6. Xóa todo");
            Console.WriteLine(" 7. Tìm kiếm todo");
            Console.WriteLine(" 8. Xem thống kê");
            Console.WriteLine(" 9. Thoát");
            Console.Write("\nNhập lựa chọn của bạn: ");
        }

        private void AddNewTodo()
        {
            Console.Write("\nNhập tiêu đề: ");
            string title = Console.ReadLine();

            Console.Write("Nhập mô tả: ");
            string description = Console.ReadLine();

            Console.WriteLine("\nChọn mức độ ưu tiên:");
            Console.WriteLine("1. Thấp");
            Console.WriteLine("2. Trung bình");
            Console.WriteLine("3. Cao");
            Console.Write("Lựa chọn (1-3): ");
            
            Priority priority = Priority.Medium;
            if (int.TryParse(Console.ReadLine(), out int p) && p >= 1 && p <= 3)
            {
                priority = (Priority)p;
            }

            manager.AddTodo(title, description, priority);
        }

        private void MarkTodoComplete()
        {
            Console.Write("\nNhập ID todo cần đánh dấu hoàn thành: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                manager.CompleteTodo(id);
            }
            else
            {
                Console.WriteLine("ID không hợp lệ!");
            }
        }

        private void EditExistingTodo()
        {
            Console.Write("\nNhập ID todo cần sửa: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                Console.Write("Nhập tiêu đề mới (để trống nếu không đổi): ");
                string title = Console.ReadLine();

                Console.Write("Nhập mô tả mới (để trống nếu không đổi): ");
                string description = Console.ReadLine();

                Console.WriteLine("\nChọn mức độ ưu tiên mới:");
                Console.WriteLine("1. Thấp");
                Console.WriteLine("2. Trung bình");
                Console.WriteLine("3. Cao");
                Console.Write("Lựa chọn (1-3): ");
                
                Priority priority = Priority.Medium;
                if (int.TryParse(Console.ReadLine(), out int p) && p >= 1 && p <= 3)
                {
                    priority = (Priority)p;
                }

                manager.EditTodo(id, title, description, priority);
            }
            else
            {
                Console.WriteLine("ID không hợp lệ!");
            }
        }

        private void DeleteExistingTodo()
        {
            Console.Write("\nNhập ID todo cần xóa: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                Console.Write("Bạn có chắc chắn muốn xóa? (y/n): ");
                if (Console.ReadLine().ToLower() == "y")
                {
                    manager.DeleteTodo(id);
                }
                else
                {
                    Console.WriteLine("Đã hủy thao tác xóa.");
                }
            }
            else
            {
                Console.WriteLine("ID không hợp lệ!");
            }
        }

        private void SearchForTodos()
        {
            Console.Write("\nNhập từ khóa tìm kiếm: ");
            string keyword = Console.ReadLine();
            manager.SearchTodos(keyword);
        }
}