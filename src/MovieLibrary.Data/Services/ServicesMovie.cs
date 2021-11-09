using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieLibrary.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace MovieLibrary.Data
{
    abstract class ServicesMovie<TEntity> : ControllerBase, IServicesMovie<TEntity>
        where TEntity : Movie
    {

        MovieLibraryContext _movieLibraryContext;

        public ServicesMovie(MovieLibraryContext movieLibraryContext)
        {
            _movieLibraryContext = movieLibraryContext;
        }

        public void Create(TEntity entity)
        {
            if (entity != null)
            {
                _movieLibraryContext.Movies.Add(entity);
                _movieLibraryContext.SaveChanges();
            }
        }

        public TEntity Read(int id)
        {
            return (TEntity)_movieLibraryContext.Movies.SingleOrDefault(p => p.Id == id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return (IEnumerable<TEntity>)_movieLibraryContext.Movies.ToList();
        }

        public void Update(int id, TEntity updateEntity)
        {
            var entity = Read(id);

            if (entity == null)
            {
                throw new Exception("Record with ID:" + id + " couldn't be found");
            }

            _movieLibraryContext.Movies.Update(updateEntity);
            //_movieLibraryContext.Entry(updateEntity).State = EntityState.Modified;
            _movieLibraryContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = Read(id);

            if (entity == null)
            {
                throw new Exception("Record with ID:" + id + " couldn't be found");
            }
            _movieLibraryContext.Movies.Remove(entity);
            //_movieLibraryContext.Entry(entity).State = EntityState.Deleted;
            _movieLibraryContext.SaveChanges();
        }
    }
}
