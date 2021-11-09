using MovieLibrary.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibrary.Data
{
    interface IServicesCategory<TEntity>
        where TEntity : Category
    {
        void Create(TEntity entity);
        TEntity Read(int id);
        IEnumerable<TEntity> GetAll();
        void Update(int id, TEntity updateEntity);
        void Delete(int id);
    }
}
