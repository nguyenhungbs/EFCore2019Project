using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using EFCore2019.Domain.Entities;
using EFCore2019.Domain.Models.Users;
using EFCore2019.Domain.Repositories;
using EFCore2019.Libraries.Utils;

namespace EFCore2019.Domain.Services.Users.Implementation
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IUserRepository _userRepository;

        public AuthorizationService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public UserModel GetById(long userId)
        {
            var entity = _userRepository.FindById(userId);
            return entity.CloneToModel<User, UserModel>();
        }

        public async Task<UserModel> GetByIdAsync(long userId)
        {
            var entity = await _userRepository.FindByIdAsync(userId);
            return entity.CloneToModel<User, UserModel>();
        }

        public UserModel Single(Expression<Func<User, bool>> expression)
        {
            var entity = _userRepository.Find(expression);
            return entity.CloneToModel<User, UserModel>();
        }

        public async Task<UserModel> SingleAsync(Expression<Func<User, bool>> expression)
        {
            var entity = await _userRepository.FindAsync(expression);
            return entity.CloneToModel<User, UserModel>();
        }

        public bool VerifyPassword(string userName, string encryptedPassword, ref UserModel userModel)
        {
            var user = _userRepository.Find(u => u.Email == userName || u.Mobile == userName);
            if (user == null) return false;
            if(user.Password == encryptedPassword)
            {
                userModel = user.CloneToModel<User, UserModel>();
                return true;
            }
            userModel = null;
            return false;
        }
    }
}
