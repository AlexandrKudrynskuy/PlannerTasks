using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Repository
{
    public interface IRepositoryRemoveUpdate<TEntity>:IRepository<TEntity>
    {
        Task<OperationDetail> DeleteAsync(int id);
        Task<OperationDetail> UpdateAsync(int id,TEntity entity);

    }
}
