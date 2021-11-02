using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StonkMarket.Models
{
    public class Stonk
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
        public decimal OneYear { get { return CalculateTotalWithCompoundInterestForOne(Price); } set { } }
        public decimal FiveYear { get { return CalculateTotalWithCompoundInterestForFive(Price); }  set { } }
        public decimal TenYear { get { return CalculateTotalWithCompoundInterestForTen(Price); } set { } }
        public UserStonk UserStonk { get; set; }
        public UserProfile userProfile { get; set; }

        decimal CalculateTotalWithCompoundInterestForOne(decimal principal)
        {
            var answer = principal * (decimal)Math.Pow((double)(1 + .1 / 1), 1 * 1);

            return answer;
        }

        decimal CalculateTotalWithCompoundInterestForFive(decimal principal)
        {
            var answer =  principal * (decimal)Math.Pow((double)(1 + .1 / 1), 1 *5);

            return answer;
        }

        decimal CalculateTotalWithCompoundInterestForTen(decimal principal)
        {
            var answer = principal * (decimal)Math.Pow((double)(1 + .1 / 1), 1 * 10);

            return answer;
        }
    }
}
