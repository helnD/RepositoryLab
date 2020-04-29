using System;
using System.Collections.ObjectModel;
using Domain.Entities;
using GalaSoft.MvvmLight.Command;
using ViewModel.DataAccess;

/*Здесь ViewModel означает класс, представляющий данные, нуждающиеся в отображении,
 и действия с ними в удобном для конкретной реализации виде.*/

namespace ViewModel.WpfViewModel
{
    public class MainViewModel
    {
        //В wpf реализации используется репозиторий Entity Framework Core
        private readonly IRepository<Student> _repository;

        public MainViewModel(IRepository<Student> repository)
        {
            _repository = repository;
            Students = new ObservableCollection<Student>(_repository.GetAll());

            AddStudentCommand = new RelayCommand(AddStudent);
            RemoveStudentCommand = new RelayCommand(RemoveStudent);
        }

        public ObservableCollection<Student> Students { get; set; }

        public Student SelectedStudent { get; set; }
        
        private void AddStudent()
        {

            var newStudent = new Student{Name = "Viktor", Birthday = DateTime.Now};
            
            Students.Add(newStudent);
            _repository.Add(newStudent);
            _repository.Commit();
        }

        private void RemoveStudent()
        {
            var studentToRemove = SelectedStudent;
            Students.Remove(studentToRemove);
            _repository.Remove(studentToRemove);
            _repository.Commit();

        }
        
        public RelayCommand AddStudentCommand { get; set; }
        public RelayCommand RemoveStudentCommand { get; set; }
    }
}