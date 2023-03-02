using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLL.Context;
using Domain;
using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace DLL.Repository
{
    public class PriorityRepository : BaseRepository<Priority>, IRepositoryRemoveUpdate<Priority>
    {
        public PriorityRepository(PlannerTasksContext _context) : base(_context) { }
   

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

        public async virtual Task<OperationDetail> UpdateAsync(int id, Priority entity)
        {
            try
            {
                var EntityOld = await Entities.FirstAsync(x => x.Id == id);
                EntityOld.Name = entity.Name;

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
