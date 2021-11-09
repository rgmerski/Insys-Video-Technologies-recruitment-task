using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieLibrary.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibrary.Data
{
    abstract class ServicesCategory<TEntity> : ControllerBase, IServicesCategory<TEntity>
        where TEntity : Category
    {

        MovieLibraryContext _movieLibraryContext;

        public ServicesCategory(MovieLibraryContext movieLibraryContext)
        {
            _movieLibraryContext = movieLibraryContext;
        }

        public void Create(TEntity entity)
        {
            if (entity != null)
            {
                _movieLibraryContext.Categories.Add(entity);
                _movieLibraryContext.SaveChanges();
            }
        }

        public TEntity Read(int id)
        {
            return (TEntity)_movieLibraryContext.Categories.SingleOrDefault(p => p.Id == id);
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

            _movieLibraryContext.Categories.Update(updateEntity);
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
            _movieLibraryContext.Categories.Remove(entity);
            //_movieLibraryContext.Entry(entity).State = EntityState.Deleted;
            _movieLibraryContext.SaveChanges();
        }
    }
}
