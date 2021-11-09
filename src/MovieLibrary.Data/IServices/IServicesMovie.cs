using MovieLibrary.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibrary.Data
{
    interface IServicesMovie<TEntity>
        where TEntity : Movie
    {
        //Task<TEntity> CreateAsync(TEntity entity);
        //Task<TEntity> ReadAsync(int id);
        //Task<TEntity> UpdateAsync(int id, TEntity updateEntity);
        //Task DeleteAsync(int id);

        void Create(TEntity entity);
        TEntity Read(int id);
        IEnumerable<TEntity> GetAll();
        void Update(int id, TEntity updateEntity);
        void Delete(int id);
    }
}
