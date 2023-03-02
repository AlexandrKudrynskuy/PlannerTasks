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
    public class StatusService
    {
        private readonly StatusRepository statusRepository;
        public StatusService(StatusRepository _statusRepository) => statusRepository = _statusRepository;
        public async Task<OperationDetail> CreateAsync(Status status) => await statusRepository.CreateAsync(status);
        public async Task<OperationDetail> DeleteAsync(int id) => await statusRepository.DeleteAsync(id);
        public async Task<OperationDetail> UpdateAsync(int id, Status status) => await statusRepository.UpdateAsync(id, status);
        public async Task<IReadOnlyCollection<Status>> GetAllAsync() => await statusRepository.GetALLAsync();
        public async Task<IReadOnlyCollection<Status>> GetFromCondition(Expression<Func<Status, bool>> condition) => await statusRepository.GetFromConditionAsync(condition);
    }
}
