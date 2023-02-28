using DLL.Repository;
using Domain;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class UserService
    {
        private readonly UserRepository userRepository;
        public UserService(UserRepository _userRepository) => userRepository = _userRepository;
        public async Task<OperationDetail> CreateAsync(User user) => await userRepository.CreateAsync(user);
        public async Task<OperationDetail> DeleteAsync(int id) => await userRepository.DeleteAsync(id);
        public async Task<OperationDetail> UpdateAsync(int id, User user) => await userRepository.UpdateAsync(id, user);
        public async Task<IReadOnlyCollection<User>> GetAllAsync() => await userRepository.GetALLAsync();
        public async Task<IReadOnlyCollection<User>> GetFromCondition(Expression<Func<User, bool>> condition) => await userRepository.GetFromConditionAsync(condition);
        public async Task<User> Login(string login, string password)=>(await GetFromCondition(x => x.Login == login && x.Password == password)).FirstOrDefault();
    }
}
