using System.Collections.Generic;
using System.Linq;
using DAL.RemoteStorage.Entities;
using ViewModel.DataAccess;
using Student = Domain.Entities.Student;

namespace DAL.RemoteStorage
{
    //реализация работы с удаленным сервером посредством https протокола
    public class RemoteRepository : IRepository<Student>
    {
        private readonly HttpContext _context = new HttpContext();
        private readonly string _host;
        
        private readonly List<Operation> _operations = new List<Operation>();

        public RemoteRepository(string host)
        {
            _host = host;
        }

        public void Add(Student element)
        {
            _operations.Add(new Operation {Name = "Add", Argument = element});
        }

        public IReadOnlyCollection<Student> GetAll()
        {
            _context.Host = _host + "/Home/GetAll";
            var students = _context.Get<IEnumerable<Student>>();
            return students.ToList();
        }

        public Student GetById(int id)
        {
            _context.Host = _host + "/Home/GetById";
            var student = _context.Get<Student>(new Dictionary<string, string> {["id"] = id.ToString()});
            return student;
        }

        public void Edit(Student element)
        {
            _operations.Add(new Operation {Name = "Edit", Argument = element});
        }

        public void Remove(Student element)
        {
            _operations.Add(new Operation {Name = "Remove", Argument = element});
        }

        public void Commit()
        {
            _context.Host = _host + "/Home/Commit";
            _context.Post(_operations);
            _operations.Clear();
        }

        public void Rollback()
        {
            _operations.Clear();
        }
        
    }
}