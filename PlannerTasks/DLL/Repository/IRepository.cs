using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Repository
{
    public interface IRepository<TEntity>
    {
        Task<OperationDetail> CreateAsync(TEntity entity);
        Task<IReadOnlyCollection<TEntity>> GetALLAsync();
        Task<IReadOnlyCollection<TEntity>> GetFromConditionAsync(Expression<Func<TEntity, bool>> condition);
    }
}
