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
    public class PriorityService
    {
        private readonly PriorityRepository repository;
        public PriorityService(PriorityRepository _repository) => repository = _repository;
        public async Task<OperationDetail> CreateAsync(Priority obj) => await repository.CreateAsync(obj);
        public async Task<OperationDetail> DeleteAsync(int id) => await repository.DeleteAsync(id);
        public async Task<OperationDetail> UpdateAsync(int id, Priority obj) => await repository.UpdateAsync(id, obj);
        public async Task<IReadOnlyCollection<Priority>> GetAllAsync() => await repository.GetALLAsync();
        public async Task<IReadOnlyCollection<Priority>> GetFromCondition(Expression<Func<Priority, bool>> condition) => await repository.GetFromConditionAsync(condition);

    }
}
