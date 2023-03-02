using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DLL.Context;
using Domain;
using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace DLL.Repository
{
    public class MyTaskRepository : BaseRepository<MyTask>, IRepositoryRemoveUpdate<MyTask>
    {
        public MyTaskRepository(PlannerTasksContext _context) : base(_context){}

        public override async Task<IReadOnlyCollection<MyTask>> GetALLAsync() => await Entities.Include(nameof(Status)).Include(nameof(Priority)).Include(nameof(TypeTask)).Include(nameof(User)).ToListAsync().ConfigureAwait(false);
        public override async Task<IReadOnlyCollection<MyTask>> GetFromConditionAsync(Expression<Func<MyTask, bool>> condition) => await Entities.Include(nameof(Status)).Include(nameof(Priority)).Include(nameof(TypeTask)).Include(nameof(User)).Where(condition).ToListAsync().ConfigureAwait(false);

        public async virtual Task<OperationDetail> DeleteAsync(int id)
        {

            try
            {
                var entity = await Entities.FirstAsync(x => x.Id == id);
                context.Remove(entity);
                await context.SaveChangesAsync();
                return new OperationDetail { IsEror = false, Message = "Delete" };
            }
            catch (Exception ex)
            {
                return new OperationDetail { IsEror = true, Message = "Delete fatal eror", Excep = ex };
            }
        }

        public async virtual Task<OperationDetail> UpdateAsync(int id, MyTask entity)
        {
            try
            {
                var EntityOld = await Entities.FirstAsync(x => x.Id == id);
                EntityOld.Name = entity.Name;
                EntityOld.Description = entity.Description;
                EntityOld.StatusId= entity.StatusId;
                EntityOld.PriorityId = entity.PriorityId;
                EntityOld.TypeTaskId = entity.TypeTaskId;
                EntityOld.AdminId = entity.AdminId;
                EntityOld.WorkerId= entity.WorkerId;
                EntityOld.DateStart = entity.DateStart;
                EntityOld.DateFinich = entity.DateFinich;
                EntityOld.TimeSpan = entity.TimeSpan;
                context.Entry(EntityOld).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return new OperationDetail { IsEror = false, Message = "Update" };
            }
            catch (Exception ex)
            {
                return new OperationDetail { IsEror = true, Message = "Update fatal eror", Excep = ex };
            }
        }
    }
}
