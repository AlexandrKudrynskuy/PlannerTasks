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

        public override async Task<IReadOnlyCollection<MyTask>> GetALLAsync() => await Entities.Include(nameof(Status)).Include(nameof(Priority)).Include(nameof(TypeTask)).Include(x=>x.Admin).Include(x=>x.Worker).ToListAsync().ConfigureAwait(false);
        public override async Task<IReadOnlyCollection<MyTask>> GetFromConditionAsync(Expression<Func<MyTask, bool>> condition) => await Entities.Include(nameof(Status)).Include(nameof(Priority)).Include(nameof(TypeTask)).Include(x => x.Admin).Include(x => x.Worker).Where(condition).ToListAsync().ConfigureAwait(false);

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

     

        public async Task<IReadOnlyCollection<MyTask>> Filter(MyTask myTask)
        {
            var res = Entities.AsQueryable();

            if (myTask.StatusId!= null)
            {
                res = res.Where(x=>x.StatusId==myTask.StatusId);
            }
            if (myTask.PriorityId != null)
            {
                res = res.Where(x => x.PriorityId == myTask.PriorityId);
            }
            if (myTask.TypeTaskId != null)
            {
                res = res.Where(x => x.TypeTaskId == myTask.TypeTaskId);
            }

            if (myTask.AdminId != null)
            {
                res = res.Where(x => x.AdminId == myTask.AdminId);
            }

            if (myTask.WorkerId != null)
            {
                res = res.Where(x => x.WorkerId == myTask.WorkerId);
            }

            if (myTask.DateStart != null)
            {
                res = res.Where(x => x.DateStart == myTask.DateStart);
            }

            if (myTask.DateFinich != null)
            {
                res = res.Where(x => x.DateFinich == myTask.DateFinich);
            }

            if (myTask.TimeSpan != null)
            {
                res = res.Where(x => x.TimeSpan == myTask.TimeSpan);
            }


            return res.ToList();
        }
        public async virtual Task<OperationDetail> UpdateAsync(int id, MyTask entity)
        {
            try
            {
                var EntityOld = await Entities.FirstAsync(x => x.Id == id);
                EntityOld.Name = entity.Name;
                EntityOld.Description = entity.Description;
                EntityOld.StatusId = entity.StatusId;
                EntityOld.PriorityId = entity.PriorityId;
                EntityOld.TypeTaskId = entity.TypeTaskId;
                EntityOld.AdminId = entity.AdminId;
                EntityOld.WorkerId = entity.WorkerId;
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
