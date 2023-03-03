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
    public class TypeTaskService
    {
        private readonly TypeTaskRepository repository;
        public TypeTaskService(TypeTaskRepository _repository) => repository = _repository;
        public async Task<OperationDetail> CreateAsync(TypeTask obj) => await repository.CreateAsync(obj);
        public async Task<OperationDetail> DeleteAsync(int id) => await repository.DeleteAsync(id);
        public async Task<OperationDetail> UpdateAsync(int id, TypeTask obj) => await repository.UpdateAsync(id, obj);
        public async Task<IReadOnlyCollection<TypeTask>> GetAllAsync() => await repository.GetALLAsync();
        public async Task<IReadOnlyCollection<TypeTask>> GetFromCondition(Expression<Func<TypeTask, bool>> condition) => await repository.GetFromConditionAsync(condition);
    } 
}
