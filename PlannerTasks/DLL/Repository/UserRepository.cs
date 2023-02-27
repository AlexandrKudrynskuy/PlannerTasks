using DLL.Context;
using Domain;
using Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Repository
{
    public class UserRepository : BaseRepository<User>, IRepositoryRemoveUpdate<User>
    {
        public UserRepository(PlannerTasksContext _context) : base(_context) { }
        public virtual async Task<OperationDetail> DeleteAsync(int id)
        {
            try
            {
                var user = await Entities.FirstAsync(x => x.Id == id);
                context.Remove(user);
                await context.SaveChangesAsync();
                return new OperationDetail { IsEror = false, Message = "Delete" };
            }
            catch (Exception ex)
            {
                return new OperationDetail { IsEror = true, Message = "Delete fatal eror", Excep = ex };
            }
        }

        public virtual async Task<OperationDetail> UpdateAsync(int id, User user)
        {
            try
            {
                var OldUser = await Entities.FirstAsync(x => x.Id == id);
                OldUser.Name = user.Name;
                OldUser.SurName = user.SurName;
                OldUser.Phone = user.Phone;
                OldUser.Photo = user.Photo;
                OldUser.Login = user.Login;
                OldUser.Password = user.Password;
                OldUser.Email = user.Email;
                OldUser.isAdmin = user.isAdmin;
                context.Entry(OldUser).State = EntityState.Modified;
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
