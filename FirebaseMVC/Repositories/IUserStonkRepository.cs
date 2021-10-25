using StonkMarket.Models;
using System.Collections.Generic;

namespace StonkMarket.Repositories
{
    public interface IUserStonkRepository
    {
        void Add(UserStonk userStonk);
        void Delete(int userStonkId);
        List<UserStonk> GetAllUserStonks();
        UserStonk GetUserStonkById(int id);
        void Update(UserStonk userStonk);
    }
}