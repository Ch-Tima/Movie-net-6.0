using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie.Interface
{
    public interface IRepository<T>
    {
        void Add(T obj);
        Task AddAsync(T obj);

        bool Update(int Id, T newObj);
        Task<bool> UpdateAsync(int Id, T newObj);


        bool Delete(T obj);
        bool Delete(int Id);

        int GetCount();
        List<T> GetAll();
        T GetId(int Id);
        List<T> GetPart(Func<T, bool> func);
        List<T> GetPart(int startIndex, int count);

    }
}
