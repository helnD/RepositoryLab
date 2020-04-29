using System.Collections.Generic;
using System.Linq;
using ViewModel.DataAccess;
using Student = Domain.Entities.Student;

namespace DAL.EntityFramework
{
    //Обычный репозиторий для работы с базой данных с помощью Entity Framework Core
    public class EfRepository : IRepository<Student>
    {

        private StudentContext _context = new StudentContext();

        public void Add(Student element)
        {
            _context.Students.Add(element);
        }

        public IReadOnlyCollection<Student> GetAll()
        {
            return _context.Students.ToList();
        }

        public Student GetById(int id)
        {
            return _context.Students.Find(id);
        }

        public void Edit(Student student)
        {
            var oldStudent = _context.Students.Single(it => it.Id == student.Id);

            oldStudent.Birthday = student.Birthday;
            oldStudent.Name = student.Name;
            oldStudent.Grades = student.Grades;
        }

        public void Remove(Student element)
        {
            _context.Students.Remove(element);
        }
        
        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Rollback()
        {
            _context?.Dispose();
        }
    }
}