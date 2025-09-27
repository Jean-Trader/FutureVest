using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Interfaces
{
    public interface ICommonRepo<TEntity>
    {
       Task<TEntity?> CreateAsync(TEntity entity);
       Task <TEntity?> GetByIdAsync(int id);
       IQueryable<TEntity>GetAllQuery();
       Task<TEntity?> UpdateByAsync(TEntity entidy,int id);
       Task<bool>DeleteByIdAsync(int id);
       Task<List<TEntity>> GetAllList();
    }
}
