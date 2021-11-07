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
        Task<TEntity> CreateAsync(TEntity entity);
        Task<TEntity> ReadAsync(TEntity entity, bool tracking = true);
        Task<TEntity> UpdateAsync(int id, TEntity updateEntity);
        Task<TEntity> DeleteAsync(int id);
    }
}
