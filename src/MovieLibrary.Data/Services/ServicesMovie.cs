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

    [Microsoft.AspNetCore.Mvc.Route("/v1/MovieManagement/[controller]")]
    [Controller]
    abstract class ServicesMovie<TEntity> : ControllerBase, IServicesMovie<TEntity>
        where TEntity : Movie
    {

        MovieLibraryContext _movieLibraryContext;

        public ServicesMovie(MovieLibraryContext movieLibraryContext)
        {
            _movieLibraryContext = movieLibraryContext;
        }

        //public virtual async Task<TEntity> CreateAsync(TEntity entity)
        //{
        //    await _movieLibraryContext.Set<TEntity>().AddAsync(entity);
        //    await _movieLibraryContext.SaveChangesAsync();
        //    return entity;
        //}

        //public virtual async Task<TEntity> ReadAsync(int id)
        //{
        //    var query = _movieLibraryContext.Set<TEntity>().AsQueryable();
        //    return await query.FirstOrDefaultAsync(entity => entity.Id == id);
        //}

        //public virtual async Task<TEntity> UpdateAsync(int id, TEntity updateEntity)
        //{
        //    var entity = await ReadAsync(id);

        //    if (entity == null)
        //    {
        //        throw new Exception("Record with ID:" + id + " couldn't be found");
        //    }

        //    _movieLibraryContext.Entry(entity).CurrentValues.SetValues(updateEntity);
        //    _movieLibraryContext.Entry(entity).State = EntityState.Modified;

        //    // Update if any of the properties was changed
        //    if(_movieLibraryContext.Entry(entity).Properties.Any(property => property.IsModified))
        //    {
        //        await _movieLibraryContext.SaveChangesAsync();
        //    }
        //    return entity;
        //}
        //public virtual async Task DeleteAsync(int id)
        //{
        //    var entity = await ReadAsync(id);

        //    if (entity == null)
        //    {
        //        throw new Exception("Record with ID:" + id + " couldn't be found");
        //    }

        //    _movieLibraryContext.Entry(entity).State = EntityState.Deleted;

        //    await _movieLibraryContext.SaveChangesAsync();
        //}

        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Microsoft.AspNetCore.Mvc.Route("Create")]
        public void Create(TEntity entity)
        {
            if (entity != null)
            {
                _movieLibraryContext.Movies.Add(entity);
                _movieLibraryContext.SaveChanges();
            }
        }

        [Microsoft.AspNetCore.Mvc.HttpGet("{id}", Name = "Get")]
        [Microsoft.AspNetCore.Mvc.Route("Get/{id}")]
        public TEntity Read(int id)
        {
            return (TEntity)_movieLibraryContext.Movies.SingleOrDefault(p => p.Id == id);
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("Get")]
        public IEnumerable<TEntity> GetAll()
        {
            return (IEnumerable<TEntity>)_movieLibraryContext.Movies.ToList();
        }

        [Microsoft.AspNetCore.Mvc.HttpPut("{id}")]
        [Microsoft.AspNetCore.Mvc.Route("Update/{id}")]
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

        [Microsoft.AspNetCore.Mvc.HttpDelete("{id}")]
        [Microsoft.AspNetCore.Mvc.Route("Delete{id}")]
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
