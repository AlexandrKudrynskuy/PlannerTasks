using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DLL.Repository;
using Domain.Model;
using Domain;

namespace BLL.Service
{
    public class MyTaskService
    {

        private readonly MyTaskRepository repository;
        public MyTaskService(MyTaskRepository _repository) => repository = _repository;
        public async Task<OperationDetail> CreateAsync(MyTask obj) => await repository.CreateAsync(obj);
        public async Task<OperationDetail> DeleteAsync(int id) => await repository.DeleteAsync(id);
        public async Task<OperationDetail> UpdateAsync(int id, MyTask obj) => await repository.UpdateAsync(id, obj);
        public async Task<IReadOnlyCollection<MyTask>> GetAllAsync() => await repository.GetALLAsync();
        public async Task<IReadOnlyCollection<MyTask>> GetFromCondition(Expression<Func<MyTask, bool>> condition) => await repository.GetFromConditionAsync(condition);
    }
}
