using Domain.Entities;

namespace DAL.RemoteStorage.Entities
{
    //Вспомогательный класс для работы с http соединением
    public class Operation
    {
        public string Name { get; set; }
        public Student Argument { get; set; }
    }
}