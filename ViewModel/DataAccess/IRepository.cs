using System.Collections.Generic;

/*Репозиторий вынес именно в эту часть проекта, так как в противном случае
инверсия зависимостей, ради которой и создается репозиторий, будет отсутствовать.
Если поместить IRepository в DAL, то нам нужна будет ссылка на него, что и означает 
зависимость более абстрактного компонента (ViewModel) от конкретной реализации (DAL)*/

namespace ViewModel.DataAccess
{

    public interface IRepository<T>
    {
        
        //Репозиторий
        void Add(T element);
        IReadOnlyCollection<T> GetAll();
        T GetById(int id);
        void Edit(T element);
        void Remove(T element);
        
        //Транзакционная часть
        void Commit();
        void Rollback();
    }

}