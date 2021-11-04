using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StonkMarket.Models
{
    public class Stonk
    {
        public int Id { get; set; }

        [DisplayName("Company")]
        public string Name { get; set; }
        public decimal Price { get; set; }
        [DisplayName("Price")]
        public decimal FlutterPrice { get { return RandomizePrice(Price); } set { } }

        [DisplayFormat(DataFormatString = "{0:MMM dd, yyyy}")]
        public DateTime Date { get; set; }
        [DisplayName("1 Year Projection")]
        public decimal OneYear { get { return CalculateTotalWithCompoundInterestForOne(RandomizePrice(Price)); } set { } }
        [DisplayName("5 Year Projection")]
        public decimal FiveYear { get { return CalculateTotalWithCompoundInterestForFive(RandomizePrice(Price)); }  set { } }
        [DisplayName("10 Year Projection")]
        public decimal TenYear { get { return CalculateTotalWithCompoundInterestForTen(RandomizePrice(Price)); } set { } }
        public UserStonk UserStonk { get; set; }
        public UserProfile userProfile { get; set; }



           decimal RandomizePrice(decimal Price)
        {
            string number = DateTime.Now.ToString("hh:mm:ss");
            if (number.EndsWith('1'))
            {
                return Price += 5;
            }
            if (number.EndsWith('4') && Price > 10)
            {
                return Price -= 17 / 4;
            }
            else if (number.EndsWith('3'))
            {
                return Price += 47 / 23;
            }
            else if (number.EndsWith('5') && Price > 5)
            {
                return Price  -= 2;
            }
            else if (number.EndsWith('9'))
            {
                return Price += 2 / 3;
            }
            else if (number.EndsWith('2') && Price > 5)
            {
                return Price -= 9 / 7;
            }
            else if (number.EndsWith('6') && Price > 5)
            {
                return Price -= 1;
            }
            else if (number.EndsWith('8'))
            {
                return Price += 5 / 3;
            }
            else return Price;
        }




        decimal CalculateTotalWithCompoundInterestForOne(decimal RandomizePrice)
        {
           
            var answer = RandomizePrice * (decimal)Math.Pow((double)(1.1), 1 * 1);

            return answer;
        }

        decimal CalculateTotalWithCompoundInterestForFive(decimal RandomizePrice)
        {
            var answer = RandomizePrice * (decimal)Math.Pow((double)(1.1), 1 * 5);

            return answer;
        }

        decimal CalculateTotalWithCompoundInterestForTen(decimal RandomizePrice)
        {
            var answer = RandomizePrice * (decimal)Math.Pow((double)(1.1), 1 * 10);

            return answer;
        }
    }
}
