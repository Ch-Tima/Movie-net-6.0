using Microsoft.EntityFrameworkCore;
using Movie.Interface;
using Movie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie.Context.Repository
{
    public class SerialRepository : IRepository<Serial>
    {
        private readonly MovieContext _movieContext;
        public SerialRepository(MovieContext mc) => _movieContext = mc;

        public void Add(Serial obj)
        {
            if (obj == null)
                return;

            _movieContext.Serials.Add(obj);
            _movieContext.SaveChanges();
        }
        public async Task AddAsync(Serial obj)
        {
            if (obj == null)
                return;

            await _movieContext.Serials.AddAsync(obj);
            await _movieContext.SaveChangesAsync();

        }


        public bool Delete(Serial obj)
        {
            throw new NotImplementedException();
        }
        public bool Delete(int Id)
        {
            var t = _movieContext.Serials.FirstOrDefault(x => x.Id == Id);
            if (t != null)
            {
                _movieContext.Serials.Remove(t);
                _movieContext.SaveChanges();
                return true;
            }
            return false;
        }


        public int GetCount()
        {
            return _movieContext.Serials.Count();
        }
        public List<Serial> GetAll() => _movieContext.Serials.ToList();
        public async Task<List<Serial>> GetAllAsync() => await _movieContext.Serials.ToListAsync();

        public Serial GetId(int Id)
        {
            return _movieContext.Serials.FirstOrDefault(x => x.Id == Id);
        }

        public List<Serial> GetPart(Func<Serial, bool> func)
        {
            return _movieContext.Serials.Where(func).ToList();
        }

        public List<Serial> GetPart(int startIndex, int count)
        {
            return _movieContext.Serials.Skip(startIndex).Take(count).ToList();
        }



        public bool Update(int Id, Serial newObj)
        {
            throw new NotImplementedException();
        }
        public Task<bool> UpdateAsync(int Id, Serial newObj)
        {
            throw new NotImplementedException();
        }
    }
}
