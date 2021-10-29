using StonkMarket.Models;
using System.Collections.Generic;

namespace StonkMarket.Repositories
{
    public interface IStonkRepository
    {
        List<Stonk> GetAllStonks();

        void DeleteStonk(int stonkId);
        Stonk GetStonkById(int id);
        void AddStonkToUserStonk(int stonkId, int userId);
    }
}