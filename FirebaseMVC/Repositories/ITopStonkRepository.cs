using StonkMarket.Models;
using System.Collections.Generic;

namespace StonkMarket.Repositories
{
    public interface ITopStonkRepository
    {
        void Add(TopStonk topStonk);
        void Delete(int topStonkId);
        List<TopStonk> GetAllTopStonks();
        TopStonk GetTopStonkById(int id);
        void Update(TopStonk topStonk);
    }
}