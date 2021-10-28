using StonkMarket.Models;
using System.Collections.Generic;

namespace StonkMarket.Repositories
{
    public interface IStonkRepository
    {
        List<Stonk> GetAllStonks();
        void AddStonk(Stonk stonk);
        void DeleteStonk(int stonkId);
        Stonk GetStonkById(int id);
        void UpdateStonk(Stonk stonk);
    }
}