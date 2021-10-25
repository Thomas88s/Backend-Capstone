using StonkMarket.Models;
using System.Collections.Generic;

namespace StonkMarket.Repositories
{
    public interface IStonkRepository
    {
        List<Stonk> GetAllStonks();
        void Add(Stonk stonk);
        void Delete(int stonkId);
        Stonk GetStonkById(int id);
        void Update(Stonk stonk);
    }
}