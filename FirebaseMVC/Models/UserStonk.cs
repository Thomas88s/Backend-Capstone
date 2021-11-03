
namespace StonkMarket.Models
{
    public class UserStonk
    {
        public int Id { get; set; }
        public int StonkId { get; set; }
        public int UserId { get; set; }
        public bool TopPerformer { get; set; }
        public int NumberOfStonks { get; set;}
        public Stonk Stonk { get; set; }
        public UserProfile UserProfile { get; set; }
        public static class ClaimTypes { }
        public decimal TotalPrice { get { return calculatedTotal(NumberOfStonks, Stonk.Price); } set { } }

        decimal calculatedTotal(int NumberOfStonks, decimal Price)
        {
            var total = NumberOfStonks * Price;
            return total;
        }
        
    }
}
