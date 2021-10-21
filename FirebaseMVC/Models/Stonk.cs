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
        public decimal OneYear { get; set; }
        public decimal FiveYear { get; set; }
        public decimal TenYear { get; set; }
    }
}
