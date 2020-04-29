using System;
using DAL.RemoteStorage;
using ViewModel.ConsoleViewModel;

namespace ConsoleApp
{
    class Program
    {
        private static readonly MainViewModel _viewModel = new MainViewModel(new RemoteRepository("https://labreposerver.azurewebsites.net"));
        
        //Реализован небольшой командный интерфейс взаимодействия с пользователем
        //id -- Возвращает студента по id
        //all -- Выводит список всех студентов в формате "{id} {name} [{grades}]
        //add <name> -- Добавляет нового пользователя (но не вносит изменения на сервер)
        //remove <id> -- Удаляет полльзователя по id (но не вносит изменения на сервер)
        //edit <id> <new name> -- Обновляет имя пользователя по id (но не вносит изменения на сервер)
        //confirm -- отправляет изменения на сервер
        //rollback -- отменяет изменения
        //addgrade <id> <grade> -- добавляет оценку пользователю 
        //average -- выводит средний балл всех студентов
        //ages -- выводит возраст всех студентов
        //update -- обновляет список студентов
        
        static void Main(string[] args)
        {

            var command = "";

            while (command != "end")
            {
                Console.Write('~');
                command = Console.ReadLine();
                var name = command.Split(' ')[0];
                switch (name)
                {
                    case "id":
                        Console.WriteLine(GetById(command));
                        break;
                    case "all":
                        Console.WriteLine(GetAll());
                        break;
                    case "add":
                        Add(command);
                        break;
                    case "remove":
                        Remove(command);
                        break;
                    case "edit":
                        Edit(command);
                        break;
                    case "confirm":
                        Confirm();
                        break;
                    case "rollback":
                        Rollback();
                        break;
                    case "average":
                        Console.WriteLine(AverageGrade());
                        break;
                    case "ages":
                        Console.WriteLine(AgeOfAllStudent());
                        break;
                    case "addgrade":
                        AddGrade(command);
                        break;
                    case "update":
                        UpdateStudents();
                        break;
                    default:
                        Console.WriteLine("Такой команды не существует");
                        break;
                }
            }
        }

        private static string GetById(string command)
        {
            var id = int.Parse(command.Split(' ')[1]);
            return _viewModel.GetById(id);
        }
        
        private static string GetAll()
        {
            return _viewModel.GetAll();
        }
        
        private static void Add(string command)
        {
            var name = command.Split(' ')[1];
            _viewModel.Add(name);
        }
        
        private static void Remove(string command)
        {
            var id = int.Parse(command.Split(' ')[1]);
            _viewModel.Remove(id);
        }

        private static void Edit(string command)
        {
            var @params = command.Split(' ');
            var id = int.Parse(@params[1]);
            var name = @params[2];
            
            _viewModel.Edit(id, name);
        }

        private static void Confirm()
        {
            _viewModel.Confirm();
        }

        private static void Rollback()
        {
            _viewModel.Rollback();
        }

        private static void AddGrade(string command)
        {
            var @params = command.Split(' ');
            var id = int.Parse(@params[1]);
            var grade = int.Parse(@params[2]);
            
            _viewModel.AddGrade(id, grade);
        }

        private static string AverageGrade()
        {
            return _viewModel.AverageGrades();
        }
        
        private static string AgeOfAllStudent()
        {
            return _viewModel.AgeOfAllStudent();
        }

        private static void UpdateStudents()
        {
            _viewModel.UpdateStudents();
        }

    }
}