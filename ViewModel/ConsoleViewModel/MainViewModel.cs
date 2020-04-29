using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using ViewModel.DataAccess;

/*Здесь ViewModel означает класс, представляющий данные, нуждающиеся в отображении,
 и действия с ними в удобном для конкретной реализации виде.*/

namespace ViewModel.ConsoleViewModel
{
    public class MainViewModel
    {
        //В консольной версии используется репозиторий, как удаленный web-сервер
        //Взаимодействие происходит по https протоколу
        //Web-сервер развернут на облаке azure
        
        private IRepository<Student> _repository;
        private List<Student> _students;

        public MainViewModel(IRepository<Student> repository)
        {
            _repository = repository;
            _students = _repository.GetAll().ToList();
        }

        public string GetById(int id)
        {
            return _repository.GetById(id).ToString();
        }

        public void Add(string name)
        {
            var id = _students.Any(it => it.Id < 0) ? _students.Min(it => it.Id) - 1 : -1;
            var newStudent = new Student {Id = id, Name = name, Birthday = DateTime.Now};
            _students.Add(newStudent);
            _repository.Add(newStudent);
        }

        public void Remove(int id)
        {
            var toRemove = _students.Single(it => it.Id == id);
            _students.Remove(toRemove);
            if (id > 0) _repository.Remove(toRemove);
        }

        public void Edit(int id, string name)
        {
            var toEdit = _students.Single(it => it.Id == id);
            toEdit.Name = name;

            if (id > 0)
            {
                var edited = new Student
                {
                    Id = toEdit.Id,
                    Name = name,
                    Birthday = toEdit.Birthday,
                    Grades = toEdit.Grades
                };
                
                _repository.Edit(edited);
            }
        }

        public string GetAll()
        {
            var result = "";

            _students.ForEach(it =>
            {
                result += it.Id + " " + it.Name + "[";
                
                var index = 1;
                foreach (var grade in it.Grades)
                {
                    result += grade;
                    if (index == it.Grades.Count) break;
                    result += ", ";
                    index++;
                }

                result += "]\n";
            });

            return result;
        }

        public void Confirm()
        {
            _repository.Commit();
            UpdateStudents();
        }

        public void Rollback()
        {
            _repository.Rollback();
            UpdateStudents();
        }

        public string AgeOfAllStudent()
        {
            var result = "";

            foreach (var student in _students)
            {
                var age = (DateTime.Now - student.Birthday).Days / 365;
                result += $"{student.Id} {student.Name} {age}\n";
            }

            return result;
        }

        public void AddGrade(int id, int grade)
        {
            var student = _students.Single(it => it.Id == id);
            var grades = student.Grades.ToList();
            grades.Add(new Grade {Value = grade});

            if (id > 0)
            {
                var newStudent = new Student
                {
                    Id = student.Id,
                    Name = student.Name,
                    Birthday = student.Birthday,
                    Grades = grades
                };
                
                _repository.Edit(newStudent);
            }
        }

        public string AverageGrades()
        {
            var result = "";

            foreach (var student in _students)
            {
                var avg = student.Grades.Count == 0 ? 0 : student.Grades.Average(it => it.Value);
                result += $"{student.Id} {student.Name} {avg}\n";
            }

            return result;
        }

        public void UpdateStudents()
        {
            _students = _repository.GetAll().ToList();
        }
    }
}