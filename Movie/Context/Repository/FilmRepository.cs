using Microsoft.EntityFrameworkCore;
using Movie.Interface;
using Movie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie.Context.Repository
{
    public class FilmRepository : IRepository<Film>
    {
        private readonly MovieContext _movieContext;
        public FilmRepository(MovieContext mc) => _movieContext = mc;

        public void Add(Film obj)
        {
            if (obj == null)
                return;

            _movieContext.Films.Add(obj);
            _movieContext.SaveChanges();
        }
        public async Task AddAsync(Film obj)
        {
            if (obj == null)
                return;

            _movieContext.Films.Add(obj);
            await _movieContext.SaveChangesAsync();
        }


        public bool Delete(Film obj)
        {
            throw new NotImplementedException();
        }
        public bool Delete(int Id)
        {
            var t = _movieContext.Films.FirstOrDefault(x => x.Id == Id);
            if(t != null)
            {
                _movieContext.Films.Remove(t);
                _movieContext.SaveChanges();
                return true;
            }
            return false;
        }

        public int GetCount()
        {
            return _movieContext.Films.Count();
        }
        public List<Film> GetAll() => _movieContext.Films.ToList();
        public Film GetId(int Id)
        {
            return _movieContext.Films.First(x => x.Id == Id);
        }
        public List<Film> GetPart(Func<Film, bool> func)
        {
            return _movieContext.Films.Where(func).ToList();
        }
        public List<Film> GetPart(int startIndex, int count)
        {
            return _movieContext.Films.Skip(startIndex).Take(count).ToList();
        }



        public bool Update(int Id, Film newObj)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(int Id, Film newObj)
        {
            throw new NotImplementedException();
        }


    }
}
