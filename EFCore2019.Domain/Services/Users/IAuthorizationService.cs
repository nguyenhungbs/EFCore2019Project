using EFCore2019.Domain.Entities;
using EFCore2019.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EFCore2019.Domain.Services.Users
{
    public interface IAuthorizationService
    {
        UserModel GetById(long userId);

        Task<UserModel> GetByIdAsync(long userId);

        Task<UserModel> SingleAsync(Expression<Func<User, bool>> expression);

        UserModel Single(Expression<Func<User, bool>> expression);

        bool VerifyPassword(string userName, string encryptedPassword, ref UserModel userModel);
    }
}
