using DLL.Context;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Repository
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected PlannerTasksContext context;
        private DbSet<TEntity> entities;
        protected DbSet<TEntity> Entities=>this.entities??=context.Set<TEntity>();
        public BaseRepository(PlannerTasksContext _context) => context = _context;  
        public virtual async Task<OperationDetail> CreateAsync(TEntity model)
        {
            try
            {
                await Entities.AddAsync(model);
                await context.SaveChangesAsync();
                return new OperationDetail { IsEror = false, Message = "Created" };
            }
            catch (Exception ex)
            {
                return new OperationDetail { IsEror = true, Message = "Create fatal eror", Excep=ex};
            }            
        }
        public virtual async Task<IReadOnlyCollection<TEntity>> GetALLAsync() => await Entities.ToListAsync().ConfigureAwait(false);
        public virtual async Task<IReadOnlyCollection<TEntity>> GetFromConditionAsync(Expression<Func<TEntity, bool>> condition) => await Entities.Where(condition).ToListAsync().ConfigureAwait(false);

     
    }
}
