using StonkMarket.Models;
using System.Collections.Generic;

namespace StonkMarket.Repositories
{
    public interface IUserStonkRepository
    {
        //void Add(UserStonk userStonk);
        void Delete(int userStonkId);
        List<UserStonk> GetAllUserStonks();
        List<UserStonk> GetTopStonks();
        UserStonk GetUserStonkById(int id);
        List<UserStonk> GetAllUserStonksById(int userProfileId);
        void AddTopStonkToUserStonk(int stonkId, int userId);
        void Update(UserStonk userStonk);
    }
}